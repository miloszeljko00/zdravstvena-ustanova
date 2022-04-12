namespace zdravstvena_ustanova.Exception
{
    class NotFoundException : System.Exception
    {
        public NotFoundException()
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(string message, System.Exception inner) : base(message, inner)
        {

        }
    }
}
