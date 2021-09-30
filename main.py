import os

from PIL import Image

with open("roomsizes.csv", "r") as f:
    roomsizes = [[int(y) for y in x.split(",")] for x in f.readlines()]

for filename in os.listdir("scs"):
    print(filename)
    im = Image.open("scs/" + filename)
    im.resize(reversed(roomsizes[int(filename.split("_")[0])]), resample=Image.NEAREST).save(
        "out/" + filename.replace("_raw", ""))
