using System;
using System.Collections.Generic;

class Solution
{
    public class treeNode
    {
        public int value;
        public treeNode left;
        public treeNode right;

        public treeNode(int val = 0, treeNode _left = null, treeNode _right = null)
        {
            value = val;
            left = _left;
            right = _right;
        }
    }

    public class Tree
    {
        // 创建二叉树
        public treeNode createTree(int[] nums)
        {
            treeNode root = new treeNode(nums[0]);
            Queue<treeNode> queue = new Queue<treeNode>();
            queue.Enqueue(root);

            int i = 0;
            int leftIdx, rightIdx;
            while (queue.Count > 0)
            {
                treeNode node = queue.Dequeue();
                if (node == null)
                    continue;

                leftIdx = 2 * i + 1;
                rightIdx = 2 * i + 2;

                if (leftIdx < nums.Length)
                {
                    treeNode lChild = nums[leftIdx] != -1 ? new treeNode(nums[leftIdx]) : null;
                    node.left = lChild;
                    queue.Enqueue(lChild);
                }

                if (rightIdx < nums.Length)
                {
                    treeNode rChild = nums[rightIdx] != -1 ? new treeNode(nums[rightIdx]) : null;
                    node.right = rChild;
                    queue.Enqueue(rChild);
                }

                i++;
            }

            return root;
        }

        // 层序遍历打印最左侧结点
        public void printLeftTreeNode(treeNode root)
        {
            Queue<treeNode> queue = new Queue<treeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                treeNode node = queue.Peek();
                Console.WriteLine(node.value + ",");

                int popCount = queue.Count;
                // 遍历该层节点，并将其左右子节点入队
                for (int i = 0; i < popCount; i++)
                {
                    node = queue.Dequeue();
                    if (node.left != null)
                        queue.Enqueue(node.left);

                    if (node.right != null)
                        queue.Enqueue(node.right);
                }
            }
        }
    }
}