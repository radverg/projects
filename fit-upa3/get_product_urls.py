#!/usr/bin/python3

import random
import time

import bs4
import requests


TARGET_PRODUCT_COUNT = 300
SERVER_BASE_URL = "https://desigourmet.es"

current_product_page = SERVER_BASE_URL + "/collections/all"
current_product_count = 0

while current_product_count < TARGET_PRODUCT_COUNT:
    page = requests.get(current_product_page)

    # Parse HTML page
    html_doc = bs4.BeautifulSoup(page.text, "html.parser")

    # Select all anchors targeting products, if no such are available, we are out of pagination range and should stop
    plinks = html_doc.find_all('a', attrs={'class': 'grid-view-item__link grid-view-item__image-container full-width-link'})
    if len(plinks) == 0:
        break  # No other products available

    # Write out the result
    for plink in plinks[:(max(0, TARGET_PRODUCT_COUNT - current_product_count))]:
        print(SERVER_BASE_URL + plink.get("href"))
        current_product_count = current_product_count + 1

    # Get URL for next page and save it for next iteration
    next_page_span = html_doc.find("span", string="Siguiente página")
    if next_page_span is None:
        break
    current_product_page = SERVER_BASE_URL + next_page_span.parent.get("href")

    # Sleep before going on a next page to lower load on the server and avoid getting eventually IP banned
    time.sleep(random.random())


