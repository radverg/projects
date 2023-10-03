#!/usr/bin/env bash

OUTPUT_FILE="output.tsv"
PRODUCT_COUNT=20

source upa_env/bin/activate
python3 get_product_urls.py | sed -n "1,${PRODUCT_COUNT}p" | python3 get_product_infos.py > $OUTPUT_FILE

