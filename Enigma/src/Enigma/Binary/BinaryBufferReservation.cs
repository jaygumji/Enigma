namespace Enigma.Binary
{
    public class BinaryBufferReservation
    {
        public int Position { get; }
        public int Size { get; }

        public BinaryBufferReservation(int position, int size)
        {
            Position = position;
            Size = size;
        }
    }
}