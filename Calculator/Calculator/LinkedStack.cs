using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class doublyLinkedList<E>
    {
        public class node<E>
        {

            private E data;
            private node<E> next;
            private node<E> prev;

            public node(E d, node<E> n, node<E> p)
            {
                this.data = d;
                this.next = n;
                this.prev = p;
            }
            public E getData()
            {
                return data;
            }
            public void SetData(E e)
            {
                data = e;
            }

            public node<E> getNext()
            {
                return next;
            }
            public void setNext(node<E> e)
            {
                this.next = e;
            }
            public node<E> getprev()
            {
                return prev;
            }
            public void setprev(node<E> e)
            {
                this.prev = e;
            }

        };

        node<E> head = null;
        node<E> tail = null;
        int size = 0;

        public doublyLinkedList()
        {

        }
        public void AddFirst(E e)
        {
            node<E> newnode = new node<E>(e, head, null);
            if (size != 0)
            {
                head.setprev(newnode);
            }
            head = newnode;
            if (size == 0)
            {
                tail = head;
            }
            size++;
        }

        public void AddLast(E e)
        {
            if(size == 0)
            {
                AddFirst(e);
            }
            else
            {
                node<E> newnode = new node<E>(e, null, tail);
                tail.setNext(newnode);
                tail = newnode;
                size++;
            }

        }

        public E RemoveFirst()
        {
            node<E> temp = head;
            head = head.getNext();
            size--;
            if (size != 0)
            {
                head.setprev(null);
            }
            if (size == 0)
            {
                tail = null;
            }
            return temp.getData();
        }

        public void AddBetween( node<E> n , E e)
        {
            n.SetData(e);
            n.getNext().getNext().getNext().setprev(n);
            n.setNext(n.getNext().getNext().getNext());
            size -= 2;
        }

        public void AddBetweenmid(node<E> n, E e)
        {
            n.SetData(e);
            n.getNext().getNext().getNext().getNext().setprev(n);
            n.setNext(n.getNext().getNext().getNext().getNext());
            size -= 3;
        }

        public void AddBetweenNeg(node<E> n, E e)
        {
            n.SetData(e);
            n.getNext().getNext().getNext().getNext().getNext().setprev(n);
            n.setNext(n.getNext().getNext().getNext().getNext().getNext());
            size -= 4;
        }

        public node<E> gethead()
        {
            return head;
        }

        public node<E> gettail()
        {
            return tail;
        }

        public int getSize()
        {
            return size;
        }

        public node<E> Deletenode(node<E> node)
        {
            node<E> temp = node.getprev();
            if (node == head)
            {
                RemoveFirst();
            }
            else if(node == tail)
            {
                tail = node.getprev();
                node.getprev().setNext(null);
                size--;
            }
            else
            {
                node.getprev().setNext(node.getNext());
                node.getNext().setprev(node.getprev());
                size--;
            }
            return temp;
        }

    };

    class LinkedStack
    {
        doublyLinkedList<char> list = new doublyLinkedList<char>();

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
        public char top()
        {
            return list.gethead().getData();
        }
    }
}
