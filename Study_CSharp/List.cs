using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicFunctionLib
{
    // 定义泛型<T>变量控制接口（interface）
    public interface IListDs<T>
    {
        int Length { get; }             //返回当前长度
        int GetLength();                //求长度
        void Clear();                   //清空操作
        bool IsEmpty();                 //判断线性表是否为空
        void Append(T item);            //附加操作
        void Insert(T item, int i);     //在第i个前面插入操作
        void InsertPost(T item, int i); //在第i个后面插入操作
        void Replace(T value, int i);   //更换内容
        T Delete(int i);                //删除操作
        T GetElem(int i);               //取表元
        int Locate(T value);            //按值查找
    }

    // 定义单链表中的成员属性
    public class Node<T>
    {
        private T data;//数据域
        private Node<T> next;//引用域
        //构造器
        public Node(T val, Node<T> p)
        {
            data = val;
            next = p;
        }
        //构造器
        public Node(Node<T> p)
        {
            next = p;
        }
        //构造器
        public Node(T val)
        {
            data = val;
        }
        //构造器
        public Node()
        {
            data = default(T);
            next = null;
        }
        //数据域属性
        public T Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        //引用域属性
        public Node<T> Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }
    }

    // 实现单链表
    public class LinkList<T> : IListDs<T>
    {
        public int Length
        {
            get
            {
                return GetLength();
            }
        }
        private Node<T> head;//单链表的头引用
        //头引用的属性
        public Node<T> Head
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
            }
        }
        //构造器
        public LinkList()
        {
            head = null;
        }

        /// <summary>
        /// 求单链表的长度
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            Node<T> p = head;
            int len = 0;
            while (p != null)
            {
                p = p.Next;
                len++;
            }
            return len;
        }

        /// <summary>
        /// 清空单链表
        /// </summary>
        public void Clear()
        {
            head = null;
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return head == null;
        }

        /// <summary>
        /// 在单链表的末尾添加新元素
        /// </summary>
        /// <param name="item"></param>
        public void Append(T item)
        {
            Node<T> q = new Node<T>(item);
            Node<T> p = new Node<T>();
            if (head == null)
            {
                head = q;
                return;
            }
            p = head;
            while (p.Next != null)
            {
                p = p.Next;
            }
            p.Next = q;
        }

        /// <summary>
        /// 在单链表第i个位置前面插入一个值为item的节点
        /// </summary>
        /// <param name="item"></param>
        /// <param name="i"></param>
        public void Insert(T item, int i)
        {
            if (IsEmpty() || i < 1)
            {
                Console.WriteLine("链表为空或者位置错误");
                return;
            }
            if (i == 1)
            {
                Node<T> q = new Node<T>(item);
                q.Next = head;
                head = q;
                return;
            }
            Node<T> p = head;
            Node<T> r = new Node<T>();
            int j = 1;
            while (p.Next != null && j < i)
            {
                r = p;
                p = p.Next;
                j++;
            }
            if (j == i)
            {
                Node<T> q = new Node<T>(item);
                Node<T> m = r.Next;
                r.Next = q;
                q.Next = m;
            }
        }

        /// <summary>
        /// 在单链表第i个位置后面插入一个值为item的节点
        /// </summary>
        /// <param name="item"></param>
        /// <param name="i"></param>
        public void InsertPost(T item, int i)
        {
            if (IsEmpty() || i < 1)
            {
                Console.WriteLine("链表为空或者位置错误");
                return;
            }
            if (i == 1)
            {
                Node<T> q = new Node<T>(item);
                q.Next = head.Next;
                head.Next = q;
                return;
            }
            Node<T> p = head;
            Node<T> r = new Node<T>();
            int j = 1;
            while (p.Next != null && j <= i)
            {
                r = p;
                p = p.Next;
                j++;
            }
            if (j == i + 1)
            {
                Node<T> q = new Node<T>(item);
                Node<T> m = r.Next;
                r.Next = q;
                q.Next = m;
            }
            else
            {
                Console.WriteLine("插入位置过大，error");
            }
        }


        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T Delete(int i)
        {
            if (IsEmpty() || i < 1)
            {
                Console.WriteLine("链表为空或者位置错误");
                return default(T);
            }
            Node<T> q = new Node<T>();
            if (i == 1)
            {
                q = head;
                head = head.Next;
                return q.Data;
            }
            Node<T> p = head;
            int j = 1;
            while (p.Next != null && j < i)
            {
                q = p;
                p = p.Next;
                j++;
            }
            if (j == i)
            {
                q.Next = p.Next;
                return p.Data;
            }
            else
            {
                Console.WriteLine("位置不正确");
                return default(T);
            }
        }

        /// <summary>
        /// 获得单链表第i个元素
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T GetElem(int i)
        {
            if (IsEmpty())
            {
                Console.WriteLine("链表是空链表");
                return default(T);
            }
            Node<T> p = new Node<T>();
            p = head;
            int j = 1;
            while (p.Next != null && j < i)
            {
                p = p.Next;
                j++;
            }
            if (j == i)
            {
                return p.Data;
            }
            else
            {
                Console.WriteLine("位置不正确！");
            }
            return default(T);

        }

        /// <summary>
        /// 在单链表中查找值为value的节点
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Locate(T value)
        {
            if (IsEmpty())
            {
                Console.WriteLine("链表是空链表！");
                return -1;
            }
            Node<T> p = new Node<T>();
            p = head;
            int i = 1;
            while ((p.Next != null) && (!p.Data.Equals(value)))
            {
                p = p.Next;
                i++;
            }
            if (p == null)
            {
                Console.WriteLine("不存在这样的节点。");
                return -1;
            }
            else
            {
                return i;
            }
        }

        /// <summary>
        /// 替换value给第i个元素
        /// </summary>
        /// <param name="value"></param>
        /// <param name="i"></param>
        public void Replace(T value, int i)
        {
            if (IsEmpty())
            {
                Console.WriteLine("链表是空链表！");
                return;
            }
            Node<T> p = new Node<T>();
            p = head;
            int j = 1;
            while ((p.Next != null) && j < i)
            {
                p = p.Next;
                j++;
            }
            if (j == i)
            {
                p.Data = value;
            }
            else
            {
                Console.WriteLine("位置不正确！");
            }
        }
    }
}