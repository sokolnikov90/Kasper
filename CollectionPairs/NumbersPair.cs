namespace CollectionPairs
{
    using System;

    [Serializable]
    public struct NumbersPair
    {
        private int a;
        private int b;

        public NumbersPair(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public int A 
        {
            get { return a; }
        }

        public int B
        {
            get { return b; }
        }

        public bool Equals(NumbersPair other)
        {
            return (this.A == other.A && this.B == other.B) || (this.A == other.B && this.B == other.A);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is NumbersPair && Equals((NumbersPair)obj);
        }
    }
}
