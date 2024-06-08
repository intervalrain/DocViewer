---
title: "[Algo] 0-4. 二叉樹(Binary Tree)"
keywords: ["C++", "Leetcode", "Binary Tree", "Algorithm", "Tree", "二叉樹"]
description: "演算法思維，介紹二叉樹這種資料結構以及如何使用它與 Leetcode 相關範例介紹"
date: 2022-10-06T23:00:28+08:00
tags: ["CS", "Algo"]
draft: false
Categories: CS
author: "Rain Hu"
showToc: true
TocOpen: true
math: true
hidemeta: false
canonicalURL: "https://intervalrain.github.io/"
disableHLJS: true
disableShare: true
disableHLJS: false
hideSummary: false
searchHidden: false
ShowReadingTime: true
ShowBreadCrumbs: true
ShowPostNavLinks: true
ShowCodeCopyButtons: true
cover:
    image: "/images/faang.webp"
    alt: "Oh! You closed up the window, so you cannot see raining"
    relative: false
    hidden: false
---
### 一、二叉樹的思維模式
+ 二叉樹的解題模式大致分為兩類：
    1. 是否可以通過遍歷一遍得解
    2. 是否可以定義一個遞迴函數，通過分治法推導出原問題的答案？
#### [[LeetCode. 104] Maximum Depth of Binary Tree(Easy)](https://leetcode.com/problems/maximum-depth-of-binary-tree/)
+ 以此題為例，可以遍歷完整個樹，並比較當下的樹的深度，得以求解。
```C++
int depth = 0;
int maxDepth(TreeNode* root){
    traverse(root, 1);
    return depth;
}
void traverse(TreeNode* root, int currDepth){
    if (!root) return;
    traverse(root->left, currDepth+1);
    depth = max(depth, currDepth);
    traverse(root->right, currDepth+1);
}
```
+ 若想辦法定義一個遞迴函數，通過分治法推導出原問題，換言之，就是先處理更小的樹，再藉由小的樹處理大的樹：
```C++
int maxDepth(TreeNode* root) {
    if (root == NULL) return 0;
    return 1 + max(maxDepth(root->left), maxDepth(root->right));
}
```
+ 事實上，兩個思維模式便對應著兩種演算法：**回溯法(back tracking)**與**動態規劃(dynamic programming)**
### 二、前序、中序、後序
+ 無論使用哪種思維模式(遍歷或找出遞迴函數)，**都要思考單獨抽出一個節點，它需要在何時(前、中、後序)做哪些事情**，其它的節點交由遞迴函數去執行相同的操作。
+ 以下我們以 quick sort 與 merge sort 為例，同樣是分治法，看看在數組上有什麼同樣的思維模式。
#### quick sort
+ 從 sort() 函式便可見類似於前序的結構。
```C++
void sort(vector<int>& nums, int left, int right){
    if (left >= right) return;              // 終止條件
    int mid = partition(nums, left, right); // 做什麼事(pre-order)
    sort(nums, left, mid-1);                // 左子樹
    sort(nums, mid+1, right);               // 右子樹
}
```
```C++
int partition(vector<int>& nums, int left, int right){
    int pivot = right;
    while (left < right){
        while (nums[left] < nums[pivot]) left++;
        while (nums[right] > nums[pivot]) right--;
        if (left < right) swap(nums[left], nums[right]);
    }
    if (left == right && nums[left] > nums[pivot] || nums[right] < nums[pivot]){
        swap(nums[left], pivot);
        return left;
    }
    return pivot;
}
```
#### merge sort
+ 從 sort() 函式便可見類似於後序的結構。
```C++
void sort(vector<int>& nums, int left, int right){
    if (right <= left) return;              // 終止條件
    int mid = left + (right-left)/2;
    sort(nums, left, mid);                  // 左子樹
    sort(nums, mid+1, right);               // 右子樹
    merge(nums, left, mid, right);          // 做什麼事(post-order)
}
```
```C++
void merge(vector<int>& nums, int left, int mid, int right){
    vector<int> vec;
    int i = left, j = mid+1;
    while (i <= mid && j <= right){
        int x = nums[i] < nums[j] ? nums[i++] : nums[j++];
        vec.push_back(x);
    }
    while (i <= mid) vec.push_back(nums[i++]);
    while (j <= right) vec.push_back(nums[j++]);
    for (int i = left; i <= right; i++)
        nums[i] = vec[i-left];
}
```
+ 換言之，以上就是一個遍歷全部節點的函式，所以本質上數組、鏈表、二叉樹都是在做同樣的事。
    + 數組
    ```C++
    void traverse(vector<int> nums, int i){
        if (i == nums.size()) return;
        // pre-order
        traverse(nums, i+1);
        // post-order
    }
    ```
    + 鏈表
    ```C++
    void traverse(ListNode* head){
        if (!head) return;
        // pre-order
        traverse(head->next);
        // post-order
    }
    ```
    + 二叉樹
    ```C++
    void traverse(TreeNode* root){
        if (!root) return;
        // pre-order
        traverse(root->left);
        // in-order
        traverse(root->right);
        // post-order
    }
    ```
### 三、層序遍歷（level-order)
+ Level-order 對應於 BFS(Breadth-First Search)，完下當下的層才會進入到下一層。
```C++
void traverse(TreeNode* root){
    queue<TreeNode*> q;
    q.push(root);
    while (!q.empty()){
        int sz = q.size();
        while (sz--){
            TreeNode* curr = q.front();
            q.pop();
            // operation
            if (curr->left) q.push(curr->left);
            if (curr->right) q. push(curr->right);
        }
    }
}
```
---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
+ 想要複習：[[Algo] 0-3. 鏈表(Linked List)](/cs/algo/linked_list)