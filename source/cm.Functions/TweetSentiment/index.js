const rp = require('request-promise');
const uuidv4 = require('uuid/v4');
const moment = require('moment');

module.exports = function (context, req) {

    const tweetInfo = req.body;

    RequestSentiment(tweetInfo.text)
        .then(sentiment => {
            var data = PrepareSentimentData(sentiment, tweetInfo);
            context.bindings.tweetEntry = data
            context.done(); 
        })
        .catch(error => {
            context.log('Error: ' + error);
            context.done();
        });
};

function RequestSentiment(text) {
    var options = {
        method: 'POST',
        uri: 'https://northeurope.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment',
        headers: {
            'Ocp-Apim-Subscription-Key': GetEnvironmentVariable('CongnitiveApiKey')
        },
        body: {
            "documents": [
                {
                    "id": "1",
                    "text": text
                }
            ]
        },
        json: true
    };

    return rp(options)
        .then(body => GetSentiment(body))
        .catch(function (err) {
            return err;
        });
}

function GetSentiment(body) {
    return parseFloat(body.documents[0].score);
}

function PrepareSentimentData(sentiment, tweetInfo) {
    return {
        PartitionKey: 'tweets',
        RowKey: GenerateKey(),
        Text: tweetInfo.text,
        Author: tweetInfo.author,
        Link: tweetInfo.link,
        Sentiment: sentiment
    }
}

function GenerateKey() {
    
    let inverted = getInvertedTimestamp();
    let uid = uuidv4();

    return inverted + '_' + uid;
}

function getInvertedTimestamp() {

    var inverted = moment('3000-01-01').unix() - moment().unix();
    var invertedString = inverted.toString();
    var pad = "000000000000000";

    return  pad.substring(0, pad.length - invertedString.length) + invertedString;
}

function GetEnvironmentVariable(name) {
    return process.env[name];
}