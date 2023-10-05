# Automatický stahovač knih z Moravské zemské kniHOVNY.
import argparse
import os
import requests
import time
import random

API_CONTENT_BASE = "https://dnnt.mzk.cz/search/iiif"
API_META_BASE = "https://dnnt.mzk.cz/search/api/v5.0/item"

parser = argparse.ArgumentParser(description='Hovna jsou mňamky.')
parser.add_argument('--session-id', dest="session_id", required=True,  type=str)
parser.add_argument('--book-id', dest="book_uid", required=True, type=str)
parser.add_argument('--destination-folder', dest="destination_folder", required=True,)
parser.add_argument('--page-delay-ms', dest="page_delay_ms", default=500, type=int)
parser.add_argument('--page-width', dest="page_width", default=4096, type=int)
parser.add_argument('--page-height', dest="page_height", default=8192, type=int)
parser.add_argument('--page-scale', dest="page_scale", default=1024, type=int)
parser.add_argument('--from-page', dest="from_page", default=0, type=int)
parser.add_argument('--to-page', dest="to_page", default=-1, type=int)
parser.add_argument('--random-ms-max', dest="random_ms_max", default=200, type=int)
args = parser.parse_args()

cookies = { 'JSESSIONID': args.session_id }
os.makedirs(args.destination_folder, exist_ok=True)

# Stáhnout seznam stránek
meta_endpoint = f"{API_META_BASE}/{args.book_uid}/children"
meta_data_json = requests.get(meta_endpoint, cookies=cookies).json()
pageids = [ pagemeta["pid"] for pagemeta in meta_data_json ]

to_page = args.to_page
if (to_page < 0 or to_page >= len(pageids)):
    to_page = len(pageids) - 1
from_page = args.from_page
print(f"Stahuje se od strany {from_page} do {to_page}. Delay je {args.page_delay_ms}ms s random přídavkem až {args.random_ms_max}ms.")

# Pro každou stránku stáhnout content a uložit do souboru
totalpages = to_page - from_page;
for index in range(from_page, to_page + 1):
    pageid = pageids[index]
    url = f"{API_CONTENT_BASE}/{pageid}/0,0,{args.page_width},{args.page_height}/{args.page_scale},/0/default.jpg"
    r = requests.get(url, cookies=cookies)
    if r.status_code == 200:
        with open(args.destination_folder + f"/page{index}.jpg", 'wb') as f:
            f.write(r.content)
        print(f"Uložena stránka {index} ({from_page}-{to_page}) ({ round(0.5 + ((index - from_page) * 100 / totalpages)) }%).")
    else:
        print(f"Bad status: {r.status_code}, requested url {url}")
    time.sleep((args.page_delay_ms + random.randint(0, args.random_ms_max)) / 1000)

print("Hotovo. Žereš hovna.")