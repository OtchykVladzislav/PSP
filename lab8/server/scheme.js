const {model, Schema} = require('mongoose')

const Lab8 = new Schema({
    name: {type: String},
    secondname: {type: String},
    thirdname: {type: String},
    tariff: {type: Number},
    volume: {type: Number},
    price: {type: Number}
})

module.exports = model('lab8', Lab8)