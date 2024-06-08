---
title: "[Algo] 3-1. Two Pointer/Sliding Window"
keywords: ["C++", "Leetcode", "Algorithm", "two pointer", "sliding window"]
description: "使用雙指標或是更進階的滑動窗口的技巧，對資料集合(可能是array，可能是list)做搜尋。"
date: 2023-03-19T22:56:03+08:00
tags: ["CS", "algo"]
draft: false
Categories: CS
author: "Rain Hu"
showToc: true
TocOpen: true
math: true
mermaid: true
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
---
> 前言：  
> 先前我們在鏈表的單元已經介紹過求**鏈表中點的「前後指針」**與求**有環鏈表的「快慢指針」**，這都是雙指針的應用。  
> 在接下來的這個章節，主要會介紹的雙指針應用，與更進階的滑動窗口(sliding window)的應用。
## 一、Two Pointer in LinkedList
+ 在本文中會學到 `LinkedList` 的七種技巧：
    1. 合併兩個有序鏈表
    2. 分解鏈表
    3. 合併多個有序鏈表
    4. 尋找鏈表的倒數第 `k` 個節點
    5. 尋找鏈表的中點
    6. 判斷鏈表是否包含環
    7. 判斷兩個鏈表是否相交
### 1. Merge Two Sorted Lists
+ [Leetcode 21. Merge Two Sorted Lists](https://leetcode.com/problems/merge-two-sorted-lists/)
![ex1-1](https://assets.leetcode.com/uploads/2020/10/03/merge_ex1.jpg)
+ 這一題的小技巧是創建一個 `dummy node` 依序將兩條鏈表中較小的值接在後面，最後回傳 `dummy->next`，過程很像 `merge sort`。
```C++
ListNode* mergeTwoLists(ListNode* list1, ListNode* list2) {
    ListNode* dummy = new ListNode();
    ListNode* curr = dummy;
    while (list1 && list2) {
        if (list1->val <= list2->val) {
            curr->next = list1;
            list1 = list1->next;
        } else {
            curr->next = list2;
            list2 = list2->next;
        }
        curr = curr->next;
    }
    if (list1) curr->next = list1;
    if (list2) curr->next = list2;
    return dummy->next;
}
```


## 二、Two Pointer in Array

## 三、Sliding Window


---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
+ 想要複習：[[Algo] 3-0. Sorting](/cs/algo/sorting)
+ 接著閱讀：[[Algo] 3-2. Binary Search](/cs/algo/binary_search)