var ip = require('ip');
const express = require('express');
const app = express();
const port = process.env.PORT || 2444;
let sqlite = require('sqlite3').verbose();
let db = new sqlite.Database('./database.db', sqlite.OPEN_READONLY, (err) => {
    if (err) console.log('Database Open Error', err.message);
    console.log('DB Connection opened successfully');
});

app.listen(port, () => {
    console.log(`Listening at ${ip.address()}:${port}`);
});

app.get('/api/songs', (req, res) => {
    switch(Object.keys(req.query)[0]) {
        case 'id':
            SendBodyById(req.query.id,res);
            break;
        case 'number':
            SendInfoByNumber(req.query.number,res);
            break;
        case 'filter':
            SendInfoByFilter(req.query.filter, res);
            break;
    }


    
});

function SendBodyById(id, res) {
    db.get(`SELECT Body
    FROM Songs
    WHERE (ID = ?);`,[id], (err, item) => {
        console.log(err ? err.message : 'Query by ID successful');
        //console.log('sent', item)
        res.send(item);
        return;
    });
}

function SendInfoByNumber(number, res) {
    db.all(`SELECT ID,
            Title,
            Number,
            Key
        FROM Songs
        WHERE (Number = $number);`, {$number: number}, (err, items) => {
            console.log(err ? err.message : 'Query by number successful');
            res.send(items);
            return;
            });
}

function SendInfoByFilter(filter, res) {
    return;
}