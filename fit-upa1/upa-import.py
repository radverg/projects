#!/usr/bin/python3

import pymongo
import pymongo.errors

import wget
import xmltodict
import time

import zipfile
import os, sys

REMOTE_FILE = "https://portal.cisjr.cz/pub/draha/celostatni/szdc/2022/GVD2022.zip"
FILENAME = "GVD2022.zip"


def insert_czptt_message(xmlroot, main_collection, helper_collection):
    """ Takes one CZPTT XML message and inserts it to the database
        If this message already exists, insertion is skipped. """
    data = xmlroot["CZPTTCISMessage"]
    pa_id = [i for i in data["Identifiers"]["PlannedTransportIdentifiers"] if i["ObjectType"] == "PA"][0]

    # Build key for the document
    docid = pa_id['Core'] + pa_id['Variant'] + pa_id['Company'] + pa_id['TimetableYear']
    data['_id'] = docid

    # Insert document to the main collection
    try:
        main_collection.insert_one(data)
    except pymongo.errors.DuplicateKeyError:  # Record already exists
        return False

    # OPTION 1: Prepare stuff in the helper collection where locations are keys.
    # Records look like this:
    #   { _id: "Okov", czptt_refs: ["KT----40878A0000542022", "KT----41294A0000542022" ...] }
    #   { _id: "Dov", czptt_refs: ["KT----40881A0000542022", "KT----41294A0000542022" ...] }
    locations = [loc["Location"]['PrimaryLocationName'] for loc in data["CZPTTInformation"]["CZPTTLocation"]]
    for loc in locations:
        if not helper_collection.find_one({"_id": loc}):  # The location does not exist yet - create it
            helper_collection.insert_one({"_id": loc, "czptt_refs": [docid]})
        else:
            helper_collection.update_one({"_id": loc}, {"$push": {"czptt_refs": docid}})

    return True


if not os.path.exists(FILENAME):
    wget.download(REMOTE_FILE)

archive = zipfile.ZipFile(FILENAME, 'r')
mongo_client = pymongo.MongoClient()
db = mongo_client.upadb
czptt_messages_col = db.czptt_messages
locations_helper_col = db.location_helpers

if "--purge" in sys.argv:
    czptt_messages_col.drop()
    locations_helper_col.drop()

counter = 1
ttotal_start = time.process_time()
for name in archive.namelist():
    if '.xml' in name:
        tstart = time.process_time()
        doc = archive.read(name)
        root = xmltodict.parse(doc)
        saved = insert_czptt_message(root, czptt_messages_col, locations_helper_col)

        message = f"[{counter}] Parsed and saved {name}, {time.process_time() - tstart:.3}s" if saved else f"[{counter}] Skipped {name}, already exists"
        print(message)
        counter = counter + 1

archive.close()

# Print stats
print(f"Total czptt messages: {czptt_messages_col.count_documents({})}")
print(f"Total locations: {locations_helper_col.count_documents({})}")
print(f"Total time: {time.process_time() - ttotal_start:.3}s")

