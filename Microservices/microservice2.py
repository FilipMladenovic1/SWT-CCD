import random

from fastapi import FastAPI

app = FastAPI()


@app.get("/")
def read_root():

    luckyNumber = random.randint(0,100);

    return {"Todays lucky number is":luckyNumber}


