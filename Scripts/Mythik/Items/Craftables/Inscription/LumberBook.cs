using Scripts.Mythik.Gumps;
using Scripts.Mythik.Items.Misc;
using Server;
using Server.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Items.Craftables.Inscription
{

    public class LumberBook : Item , ResourceBook
    {
        private Dictionary<Type, ResourceStub> m_StoredItems = new Dictionary<Type, ResourceStub>();

        public Dictionary<Type, ResourceStub> StoredItems { get { return m_StoredItems; } }
        private readonly uint MAX_STORAGE_AMOUNT = 50000;

        [Constructable]
        public LumberBook() : base(0x2252)
        {
            this.Name = "lumber storage book";
            this.Hue = 0x43;
        }

        public LumberBook(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 1))
                from.SendGump(new ResourceStorageBookGump(from, this, ResourceStorageGumpPage.Regular, 0));

        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is BaseIngot)
            {
                return AddResource(from, (BaseIngot)dropped, false);
            }
            else if (dropped is Container)
            {
                Container container = (Container)dropped;

                ArrayList toDelete = new ArrayList();
                bool added = false;

                for (int i = 0; i < container.Items.Count; ++i)
                {
                    Item item = (Item)container.Items[i];

                    if (item is Log || item is BaseBoard)
                    {
                        if ((added = AddResource(from, item, true)))
                            toDelete.Add(item);
                    }
                }

                if (added)
                    from.SendMessage("You add ingots from the container into the book.");

                for (int j = 0; j < toDelete.Count; ++j)
                    ((Item)toDelete[j]).Delete();

                if (container.Items.Count == 0)
                    return true;
            }
            else
            {
                from.SendLocalizedMessage(1042276); // You cannot drop that there.
            }

            return base.OnDragDrop(from, dropped);
        }

        public bool AddResource(Mobile from, Item reagent, bool passive)
        {
            if (!m_StoredItems.ContainsKey(reagent.GetType()))
                m_StoredItems.Add(reagent.GetType(), new ResourceStub() { m_Type = reagent.GetType(), m_Count = 0 });
            var resource = m_StoredItems[reagent.GetType()];
            if (resource.m_Count >= MAX_STORAGE_AMOUNT)
            {
                if (!passive)
                    from.SendMessage("This book cannot hold more of that resource.");
                return false;
            }

            var newTotal = resource.m_Count + reagent.Amount;
            if(newTotal > MAX_STORAGE_AMOUNT)
            {
                //partial deposit
                var excess = newTotal - MAX_STORAGE_AMOUNT;
                var toAdd = (uint)(reagent.Amount - (newTotal - MAX_STORAGE_AMOUNT));
                if (toAdd > reagent.Amount)
                    return false;
                m_StoredItems[reagent.GetType()].m_Count += toAdd;
                reagent.Amount -= (int)toAdd;
                if (!passive)
                    from.SendMessage("You were only able to add {0} of the resource.", toAdd);
                return false;
            }
            else
            {
                m_StoredItems[reagent.GetType()].m_Count += (uint)reagent.Amount;
                reagent.Delete();
                if (!passive)
                    from.SendMessage("You add all of the resources into the book.");
                return true;
            }
            // InvalidateProperties();
        }

    

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

           // for (int i = 0; i < 15; ++i)
           //     if (m_Reagents[i] > 0)
           //         list.Add(1080521 + i, "{0}", m_Reagents[i]);
        }


        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(m_StoredItems.Count);

            foreach(var kv in m_StoredItems)
            {
                writer.Write(kv.Key.FullName);
                writer.Write(kv.Value.m_Count);
            }
                           
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            int cnt = reader.ReadInt();
            for (int i = 0; i < cnt; ++i)
            {
                var typ = Type.GetType(reader.ReadString());
                m_StoredItems.Add(typ, new ResourceStub() { m_Count = reader.ReadUInt(), m_Type = typ });
            }

        }
    }
}
