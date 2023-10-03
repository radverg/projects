#!/usr/bin/python3

import random
import time

import bs4
import requests

import sys

MAX_PRODUCT_COUNT = 500
BATCH_SIZE = 15

SLEEP_ITEM = 1
SLEEP_BATCH = 30

counter = 0 # Item counter

for line in sys.stdin:
	url = line.rstrip('\n')
	page = requests.get(url)
	
	# Check correct status code
	if page.status_code != 200:
		time.sleep(SLEEP_ITEM + random.random())
		continue

	# Parse HTML page
	html_doc = bs4.BeautifulSoup(page.text, "html.parser")

	# Get tags with info
	name_meta = html_doc.find("meta", property="og:title")
	price_meta = html_doc.find("meta", property="og:price:amount")

	# Check if tags were found
	if name_meta == None or price_meta == None:
		time.sleep(SLEEP_ITEM + random.random())
		continue

	# Parse content from tags
	name = name_meta["content"]
	price = price_meta["content"]

	# Print output TSV
	print(url + '\t' + name + '\t' + price)

	# counter++
	counter = counter + 1	

	# Sleep before next item
	time.sleep(SLEEP_ITEM + random.random())

	# Exit loop if maximum product were parsed	
	if counter >= MAX_PRODUCT_COUNT:
		break 
	
	# Sleep longer after every batch
	if counter % BATCH_SIZE == 0:
		time.sleep(SLEEP_BATCH + random.random()*10)

