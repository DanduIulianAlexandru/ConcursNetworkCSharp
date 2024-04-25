using System;

namespace ConcursServices; 

public class ConcursException : Exception{
    public ConcursException():base() { }

    public ConcursException(String msg) : base(msg) { }

    public ConcursException(String msg, Exception ex) : base(msg, ex) { }
}