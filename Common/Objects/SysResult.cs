using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Objects
{
    public class SysResult
    {
        public bool IsSuccess { get; set; }
        public object Value { get; set; }
        public string Message { get; set; }
    }
    //===============================================================
    public class SysResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
    //===============================================================
}