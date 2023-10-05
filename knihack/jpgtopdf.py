import os
import img2pdf
import glob
import sys

if (len(sys.argv) < 2):
    print("Zadej cestu ke složce s jpg soubory.")
    exit()

path = sys.argv[1];

def my_key(fl):
    return int(fl [ (fl.find("page") + 4):-4 ])

files = glob.glob(f"{path}/*.jpg") 
files.sort(key=my_key)

print(f"Spojuji {len(files)} souborů do {path}/book.pdf")
with open(f"{path}/book.pdf", "wb") as f:
    f.write(img2pdf.convert(files))

print("Hotovo. Hovno upečeno.")
