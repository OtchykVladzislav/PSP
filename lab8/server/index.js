const cors = require('cors')
const express = require('express')
const app = express()
const mongoose = require('mongoose')
const PORT = 8000
const link = 'mongodb://127.0.0.1:27017/Labs'
const Lab8 = require('./scheme')


app.use(express.json())
app.use(cors())

app.get('/list', async (req, res) => {
    const list = await Lab8.find() 
    res.send(list)
})

app.get('/list/:id', async (req, res) => {
    const list = await Lab8.findById(req.params.id)
    res.send(list)
})

app.post('/list', async (req, res) => {
    const list = await Lab8.collection.insertOne({...req.body})
    res.send(list)
})

app.delete('/list/:id', async (req, res) => {
    const list = await Lab8.deleteOne({ _id: req.params.id })
    res.send('Success')
})

app.put('/list/:id', async (req, res) => {
    const list = await Lab8.findOneAndUpdate({ _id: req.params.id }, req.body, {
        new: true,
        upsert: true,
        rawResult: true // Return the raw result from the MongoDB driver
    })
    res.send(list)
})

async function connect(){
    try{
        await mongoose.connect(link)
        console.log('DB connect')
        app.listen(PORT, () => console.log('Server start'))
    }
    catch{
        console.log()
    }
}

connect()