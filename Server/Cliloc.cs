//CliLocDAO.cs - the data access object used to read/write cliloc files
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection; 				//needed for ConstructorInfo
using System.Text;


namespace Server
{
    public class DirectObjectPropertyList : CollectionBase
    {
        public DirectObjectPropertyList(ObjectPropertyList opl)
        {
            //parse the data from the object property list
            //note: this code is adapted from Xanthor's Auction system

            if (opl == null)
            {
                return;
            }

            //since the object property list is based on a packet object, the property info is packed away in a packet format
            byte[] data = opl.UnderlyingStream.UnderlyingStream.ToArray();

            int index = 15; // First localization number index

            while (true)
            {
                //reset the number property
                uint number = 0;

                //if there's not enough room for another record, quit
                if (index + 4 >= data.Length)
                {
                    break;
                }

                //read number property from the packet data
                number = (uint)(data[index++] << 24 | data[index++] << 16 | data[index++] << 8 | data[index++]);

                //reset the length property
                ushort length = 0;

                //if there's not enough room for another record, quit
                if (index + 2 > data.Length)
                {
                    break;
                }

                //read length property from the packet data
                length = (ushort)(data[index++] << 8 | data[index++]);

                //determine the location of the end of the string
                int end = index + length;

                //truncate if necessary
                if (end >= data.Length)
                {
                    end = data.Length - 1;
                }

                //read the string into a StringBuilder object
                StringBuilder s = new StringBuilder();
                while (index + 2 <= end + 1)
                {
                    short next = (short)(data[index++] | data[index++] << 8);

                    if (next == 0)
                        break;

                    s.Append(Encoding.Unicode.GetString(BitConverter.GetBytes(next)));
                }

                //add this data to the list
                Add(new DOPLEntry((int)number, s.ToString()));
            }
        }

        protected override void OnClear()
        {
            base.OnClear();
        }


        //ICollection members
        public virtual bool IsSynchronized
        {
            get { return false; }
        }

        public virtual object SyncRoot
        {
            get { return List.SyncRoot; }
        }

        public virtual void CopyTo(DOPLEntry[] array, int index)
        {
            List.CopyTo(array, index);
        }

        //IList members
        public virtual bool IsFixedSize
        {
            get { return false; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual DOPLEntry this[int index]
        {
            get { return (DOPLEntry)(List[index]); }
        }

        public virtual int Add(DOPLEntry entry)
        {
            int add = List.Add(entry);
            return add;
        }

        public virtual bool Contains(DOPLEntry entry)
        {
            return List.Contains(entry);
        }

        public virtual int IndexOf(DOPLEntry entry)
        {
            return List.IndexOf(entry);
        }

        public virtual void Insert(int index, DOPLEntry entry)
        {
            List.Insert(index, entry);
        }

        public virtual void Remove(DOPLEntry entry)
        {
            List.Remove(entry);
        }
    }

    public class DOPLEntry
    {
        protected int _Index;
        protected string _Arguments;

        public int Index { get { return _Index; } }
        public string Arguments { get { return _Arguments; } }

        public DOPLEntry(int index, string arguments)
        {
            _Index = index;
            _Arguments = arguments;
        }

        //TODO: move compiling thing in here?


    }

    public class CliLoc
    {
        public static int MaxEntry { get { return 3011032; } }

        //this is used to guess the space required for cliloc text on a gump.  Basically it's the average pixels per character
        public static int PixelsPerCharacter { get { return 7; } }

        //this stores all the cliloc entries
        protected static Hashtable _CliLocs;

        public static Hashtable CliLocs
        {
            get
            {
                if (_CliLocs == null)
                {
                    Initialize();
                }

                return _CliLocs;
            }
        }

        //this loads in the cliloc data into a nice easy to use List<>
        public static void Initialize()
        {
            //NOTE: to use other regional languages, change the filename here
            CliLocDAO cdao = new CliLocDAO("cliloc.enu");
            _CliLocs = cdao.Read();
        }

        //this method provides a direct access object property list for a specified object
        public static DirectObjectPropertyList GetDirectPropertyList(object obj)
        {
            //fetch the object property list for this object
            ObjectPropertyList opl = null;

            if (obj is Item)
            {
                Item item = (Item)obj;

                opl = new ObjectPropertyList(item);
                item.GetProperties(opl);
            }
            else if (obj is Mobile)
            {
                Mobile mobile = (Mobile)obj;

                opl = new ObjectPropertyList(mobile);
                mobile.GetProperties(opl);
            }

            if (opl == null)
            {
                //if there was a problem with this process, just return null
                return null;
            }

            return new DirectObjectPropertyList(opl);
        }

        //this method gets the name for an object from the object property list.
        public static string GetName(object obj)
        {
            return GetPropertiesList(obj)[0];
        }


        //generate the full object property list 
        public static List<string> GetPropertiesList(object obj)
        {
            if (obj is Type)
            {
                try
                {
                    //create the object
                    object typeobj = Activator.CreateInstance((Type)obj);

                    //find its name using the instanced object
                    List<string> recurseproperties = GetPropertiesList(typeobj);

                    //clean up by removing this object
                    if (typeobj is Item)
                    {
                        ((Item)typeobj).Delete();
                    }
                    else if (typeobj is Mobile)
                    {
                        ((Mobile)typeobj).Delete();
                    }

                    return recurseproperties;
                }
                catch (Exception e)
                {
                    //if there was a problem with this process, just return the type name

                    return new List<string>(new string[] { e.Message });
                }
            }

            DirectObjectPropertyList dopl = GetDirectPropertyList(obj);

            if (dopl == null)
            {
                return new List<string>(new string[] { "null" }); ;
            }

            List<string> properties = new List<string>();

            foreach (DOPLEntry doplentry in dopl)
            {
                properties.Add(CliLoc.LocToString(doplentry.Index, doplentry.Arguments));
            }

            return properties;
        }
        //the main method used for producing useful strings
        public static string LocToString(int index)
        {
            if (_CliLocs == null)
            {
                Initialize();
            }


            if (_CliLocs == null)
            {
                return "CliLoc not loaded!";
            }

            //scan through the cliloc's for the correct entry

            if (_CliLocs.ContainsKey(index))
            {
                return ((CliLocEntry)_CliLocs[index]).Text;
            }

            return null;
        }
        public static string LocToString(int index, string[] args)
        {
            if (args.Length == 0)
                return LocToString(index);

            string basestring = LocToString(index);
            int i = 0;
            //parse the string for any argument identifiers
            while (basestring != null && basestring.IndexOf("~") > -1)
            {
                //this determines the string that needs replacing
                string replacestring = FindReplace("~", basestring);


                //here's the string that will replace it
                string argstring = args[i];

                //rethreaded
                if (argstring.IndexOf("#") == 0)
                {
                    int recurseloc = Convert.ToInt32(argstring.Substring(1, argstring.Length - 1));

                    argstring = LocToString(recurseloc);
                }

                basestring = basestring.Replace(replacestring, argstring);

                i++;
                if (i < args.Length)
                    argstring = args[i];
           }

            return basestring;

        }

        //the special case, where there are arguments to insert in the string
        public static string LocToString(int index, string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                return LocToString(index);
            }

            string basestring = LocToString(index);

            //parse the string for any argument identifiers
            while (basestring != null && basestring.IndexOf("~") > -1)
            {
                //this determines the string that needs replacing
                string replacestring = FindReplace("~", basestring);

                int argsdivider = args.IndexOf("\t");

                //here's the string that will replace it
                string argstring = argsdivider == -1 ? args : args.Substring(0, argsdivider);

                //rethreaded
                if (argstring.IndexOf("#") == 0)
                {
                    int recurseloc = Convert.ToInt32(argstring.Substring(1, argstring.Length - 1));

                    argstring = LocToString(recurseloc);
                }

                basestring = basestring.Replace(replacestring, argstring);


                if (argsdivider > -1)
                    args = args.Substring(argsdivider + 1, args.Length - (argsdivider) - 1);
            }

            return basestring;

        }

        //determine the maximum string length in the collection of strings.  Useful when determining gump size, for example
        public static int GetMaxLength(List<string> strings)
        {
            if (strings == null)
            {
                return 0;
            }

            int maxlength = 0;

            foreach (string str in strings)
            {
                maxlength = Math.Max(maxlength, str.Length);
            }

            return maxlength;
        }

        //useful find and replace method when inserting arguments in a localized string
        private static string FindReplace(string key, string fromstring)
        {
            string replacestring = "";

            if (fromstring.IndexOf(key) > -1)
            {
                replacestring += key;

                //chop off up to and including the first key
                fromstring = fromstring.Substring(fromstring.IndexOf(key) + 1, fromstring.Length - fromstring.IndexOf(key) - 1);

                //grab up to the second key
                replacestring += fromstring.Substring(0, fromstring.IndexOf(key) + 1);
            }

            return replacestring;
        }
    }
    //this is an entry which holds the loaded cliloc index/text pair
    public class CliLocEntry
    {
        protected int _Index;
        protected string _Text;

        public int Index
        {
            get { return _Index; }
        }

        public string Text
        {
            get { return _Text; }
        }

        public override string ToString()
        {
            return _Text;
        }

        public CliLocEntry(int index, string text)
        {
            _Index = index;
            _Text = text;
        }
    }

    //the data access object for reading in the cliloc data file
    class CliLocDAO
    {
        static string _FilePath;
        static string _Filename;

        //private static FileStream _Index, _Stream;
        //private static GenericReader _Reader;

        public static string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        //default filename
        public CliLocDAO() : this("Cliloc.enu")
        {
        }

        //find the file path
        public CliLocDAO(string filename) : this(filename, Core.FindDataFile(filename))
        {
        }

        //master constructor, where you can specify the filename and file path
        public CliLocDAO(string filename, string filepath)
        {
            _Filename = filename;
            _FilePath = filepath;
        }

        //read operation, which loads all the data into the specified cliloc entry hashtable
        public Hashtable Read()
        {
            Hashtable clilocs = new Hashtable();

            if (_Filename == null || _Filename == "")
            {
                _Filename = "cliloc.enu";       //default filename
            }

            if (File.Exists(_FilePath))
            {
                using (FileStream stream = new FileStream(_FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    //here's where the reader is set up
                    BinaryReader reader = new BinaryReader(stream);

                    //first, read in the six header bytes to seek forward
                    for (int i = 0; i < 6; i++)
                    {
                        reader.ReadByte();
                    }

                    //now begin reading the cliloc contents.  text is encoded in UTF8 format
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;

                    int index = 0;

                    //this is the highest visible index in the file.  At least, as of the version I used to make this!					
                    while (index != CliLoc.MaxEntry)
                    {
                        //Read string from binary file with UTF8 encoding
                        index = reader.ReadInt32();

                        //read unused byte to seek reader ahead
                        reader.ReadByte();

                        //read in string length and then read the string
                        short strlen = reader.ReadInt16();
                        byte[] buffer = new byte[strlen];
                        reader.Read(buffer, 0, strlen);

                        //parse the string from the UTF8 encoded format
                        string text = encoding.GetString(buffer);


                        clilocs.Add(index, new CliLocEntry(index, text));
                    }

                }
            }
            else
            {
                Console.WriteLine("CliLoc load error: file doesn't exist");
                return null;
            }

            return clilocs;
        }


    }
}