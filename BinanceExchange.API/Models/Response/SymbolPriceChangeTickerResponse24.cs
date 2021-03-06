﻿using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response
{
    public class SymbolPriceChangeTickerResponse24
    {
        [DataMember(Order = 1)]
        public string symbol { get; set; }

        [DataMember(Order = 2)]
        public decimal PriceChange { get; set; }

        [DataMember(Order = 3)]
        public decimal PriceChangePercent { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "weightedAvgPrice")]
        public decimal WeightedAveragePercent { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "prevClosePrice")]
        public decimal PreviousClosePrice { get; set; }

        [DataMember(Order = 6)]
        public decimal LastPrice { get; set; }

        [DataMember(Order = 8)]
        public decimal lastQty { get; set; }

        [DataMember(Order = 8)]
        public decimal BidPrice { get; set; }

        [DataMember(Order = 9)]
        public decimal bidQty { get; set; }

        [DataMember(Order = 10)]
        public decimal AskPrice { get; set; }

        [DataMember(Order = 11)]
        public decimal askQty { get; set; }

        [DataMember(Order = 12)]
        public decimal OpenPrice { get; set; }

        [DataMember(Order = 13)]
        public decimal HighPrice { get; set; }

        [DataMember(Order = 14)]
        public decimal LowPrice { get; set; }

        [DataMember(Order = 12)]
        public decimal Volume { get; set; }

        [DataMember(Order = 13)]
        public decimal quoteVolume { get; set; }

        [DataMember(Order = 14)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime OpenTime { get; set; }

        [DataMember(Order = 15)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime CloseTime { get; set; }

        [DataMember(Order = 16)]
        [JsonProperty(PropertyName = "firstId")]
        public long FirstTradeId { get; set; }

        [DataMember(Order = 17)]
        [JsonProperty(PropertyName = "lastId")]
        public long LastId { get; set; }

        [DataMember(Order = 18)]
        [JsonProperty(PropertyName = "count")]
        public int TradeCount { get; set; }
    }
}
