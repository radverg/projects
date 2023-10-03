#!/usr/bin/python3

import pymongo

import time
import datetime

import argparse


# Argument parsing
parser = argparse.ArgumentParser(description='Finds soonest available connection for given day and time')

parser.add_argument('odkud', metavar='odkud', type=str, help='Starting location in string literal')
parser.add_argument('kam', metavar='kam', type=str, help='To where in string literal')
parser.add_argument('date', metavar='date', type=str, help='Date in YYYY-MM-DD format')
parser.add_argument('time', metavar='time', type=str, help='Time in HH:MM format')

args = parser.parse_args()

# Check if given date was correct and parse it
try:
    date = datetime.datetime.strptime(args.date + " " + args.time, "%Y-%m-%d %H:%M") 
except:
    print("Exited: Invalid date/time format: " + args.date + " " + args.time)
    exit(1)

# Connect to client
mongo_client = pymongo.MongoClient()
db = mongo_client.upadb

# Collections
messages = db.czptt_messages
locations_db = db.location_helpers

# Start counting processing time
total_start = time.process_time()

# Make pipelines
pipeline_odkud = [{ "$match": { "_id": args.odkud } }, ]
pipeline_kam = [{ "$match": { "_id": args.kam } }, ]

# Find from and to location by key
results_odkud = locations_db.aggregate(pipeline_odkud)
results_kam = locations_db.aggregate(pipeline_kam)

# Get connections name for each location from and to
odkuds = []
kams = []
for od in results_odkud:
    odkuds = od["czptt_refs"]
for ka in results_kam:
    kams = ka["czptt_refs"]

# Intersect their connections, only the ones in common need to be checked
connections = [value for value in odkuds if value in kams]

# Init values for storing/finding final/best connection
best = []
best_name = ""
best_time = datetime.time(23, 59, 59)

# Check each connection manually if it is available
for connection in connections:

    # Get connection by key 
    pipeline_connection = [{  "$match": { "_id": connection } }, ]
    rawdata = messages.aggregate(pipeline_connection)
    for document in rawdata:  

        # Get and parse start and end dates of connection
        start = document["CZPTTInformation"]["PlannedCalendar"]["ValidityPeriod"]["StartDateTime"]
        end = document["CZPTTInformation"]["PlannedCalendar"]["ValidityPeriod"]["EndDateTime"]
        date_start = datetime.datetime.strptime(start, "%Y-%m-%dT%H:%M:%S")
        date_end = datetime.datetime.strptime(end, "%Y-%m-%dT%H:%M:%S")

        # Check if we are in correct time range
        if not (date_start <= date <= date_end):
            continue

        # Get bitmap of days        
        bitmap = document["CZPTTInformation"]["PlannedCalendar"]["BitmapDays"]
        # Check if connection available on given date
        diff = (date - date_start).days
        if bitmap[diff] == "0":
            continue

        # Init lists for stations checking
        locations  = []
        locations_pairs  = []

        # Go through each location in connection and save relevant ones
        for location in document["CZPTTInformation"]["CZPTTLocation"]:

            # If no activity in location skip it, does not stop here
            if "TrainActivity" not in location:
                continue
            
            # Get info about time at location
            times = location["TimingAtLocation"]["Timing"]
            # Sometimes multiple timing info, get first one 
            if isinstance(times, list):
                times = times[0]
            t = times["Time"][0:8] # get time and trim time format
            td = datetime.datetime.strptime(t, "%H:%M:%S") # convert to time
 
            # Save location time and name info  
            locations_pairs.append((location["Location"]["PrimaryLocationName"], td.time()))
            locations.append(location["Location"]["PrimaryLocationName"])

        # Check correct direction of locations in connection
        if args.odkud in locations and args.kam in locations:
            if locations.index(args.kam) > locations.index(args.odkud):
                # Find starting location in locations list 
                for pair in locations_pairs:
                    # Determine if it's the closest one to given time and better than default/previous found
                    if pair[0] == args.odkud and (pair[1] < best_time and pair[1] > date.time()):
                        # if found then update best results
                        best = locations_pairs
                        best_name = connection
                        best_time = pair[1]


# Init values not changed = didn't find connection
if best_name == "":
    print("Couldn't find available connection for given date and time")
# Else print connection time table
else:
    print(best_name) # connection name

    # Only print starting from starting location to ending location
    starts = False
    for k in best:
        if k[0] == args.odkud:
            starts = True
        if starts: # print timetable
            print(str(k[1]) + " " + k[0])
        if k[0] == args.kam:
            break

# Processing info 
print(f"Total spent processing: {time.process_time() - total_start:.3}s")
