﻿using System;
using Project.Domain.Events;
using Scenario.Domain.Modeling.Attributes;

namespace Project.Domain
{
    public class Item: Entity
    {
        public Item()
        {
        }

        public int Stock { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        [ScenarioEvent(typeof(ItemPriceChangedEvent))]
        public void PriceChanged()
        {
            Raise(new ItemPriceChangedEvent(this));
        }

        [ScenarioEvent(typeof(ItemOutOfStockEvent))]
        public void OutOfStock()
        {
            Raise(new ItemOutOfStockEvent(this));
        }

        [ScenarioEvent(typeof(CreatedEvent<Item>))]
        public override void Created()
        {
            Raise(new CreatedEvent<Item>(this));
        }

        [ScenarioEvent(typeof(CreatedEvent<Item>))]
        public override void Updated()
        {
            Raise(new UpdatedEvent<Item>(this));
        }
    }
}
