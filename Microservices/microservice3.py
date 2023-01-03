from fastapi import FastAPI

app = FastAPI()

convicts = {
    1: {
        "first name": "Bonnie",
        "surname": "Parker",
        "age": 23,
        "charge": "armed robbery, theft, extortion"
    },

        2: {
        "first name": "Clyde",
        "surname": "Barrow",
        "age": 24,
        "charge": "armed robbery, theft, extortion, murder"
    },

        3: {
        "first name": "Filip",
        "surname": "Mladenovic",
        "age": 27,
        "charge": "hacking into the police station, being dangerously good looking"
    }
}

@app.get("/")
def read_root():
    return {"Police Station System": "What Convict Do You Search For?"}

@app.get("/get-convict/{convict_id}")
def get_convict(convict_id: int):
    return convicts[convict_id]


