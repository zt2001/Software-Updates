using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Software_Updates
{
    /// <summary>
    /// 主节点
    /// </summary>
    public struct ToJsonMy2
    {
        /// <summary>
        /// 浮点型
        /// </summary>
        public float floats { get; set; }
        public double doubles { get; set; }
        /// <summary>
        /// 布尔
        /// </summary>
        public bool bools { get; set; }
        /// <summary>
        /// 字符串
        /// </summary>
        public string strings { get; set; }
        public char chars { get; set; }
        /// <summary>
        /// 整数数组
        /// </summary>
        public int[] Integer_array { get; set; }
        public List<string> list { get; set; }
        /// <summary>
        /// 整数类型
        /// </summary>
        public sbyte sbytes { get; set; }
        public byte bytes { get; set; }
        public short shorts { get; set; }
        public ushort ushorts { get; set; }
        public int ints { get; set; }
        public uint uints { get; set; }
        public long longs { get; set; }
        public ulong ulongs { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public Child_node Child_node;
        public Child_node_nesting Child_node_nesting;

    }


    /// <summary>
    /// 子节点类
    /// </summary>
    public struct Child_node
    {
        public string A { get; set; }
    };

    /// <summary>
    /// 子节点中类
    /// </summary>
    public struct Child_node_nesting
    {
        public string B { get; set; }
        public Class_in_Class Class_in_Class;
    };
    /// <summary>
    /// 子节点类中类
    /// </summary>
    public struct Class_in_Class
    {
        public string C { get; set; }
    };
}
