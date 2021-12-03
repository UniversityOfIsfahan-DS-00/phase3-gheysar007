using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class LinkedStack
    {
        private class SinglyLinkedList
        {
            private class node
            {

                char data;
                node next;

                public node(char d, node n)
                {
                    this.data = d;
                    this.next = n;
                }
                public char getData()
                {
                    return data;
                }

                public node getNext()
                {
                    return next;
                }
                public void setNext(node e)
                {
                    this.next = e;
                }

            };

            node head = null;
            node tail = null;
            int size = 0;

            public SinglyLinkedList()
            {

            }
            public void AddFirst(char e)
            {
                node newnode = new node(e, head);
                head = newnode;
                if (size == 0)
                {
                    tail = head;
                }
                size++;
            }

            public char RemoveFirst()
            {
                node temp = head;
                head = head.getNext();
                size--;
                return temp.getData();
            }

            public int getSize()
            {
                return size;
            }

        };

        SinglyLinkedList list = new SinglyLinkedList();

        public LinkedStack()
        {

        }
        public int size()
        {
            return list.getSize();
        }
        public bool isEmpty()
        {
            return list.getSize() == 0;
        }
        public void push(char e)
        {
            list.AddFirst(e);
        }
        public char pop()
        {
            return list.RemoveFirst();
        }
    }
}
