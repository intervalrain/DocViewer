---
title: "[Algo] 2-1. 暴力演算法 Brute Force"
keywords: ["C++", "Leetcode", "Algorithm", "Brute Force", "暴力法"]
description: "演算法設計，介紹什麼是暴力演算法，並示範幾種資料結構的遍歷與枚舉"
date: 2023-01-24T15:57:40+08:00
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
## 一、暴力演算法
+ 暴力演算法就是枚舉法，試想今天有一個行李箱的密碼鎖為四個一組，但你又忘記密碼，那要怎麼辦？你會試著從 `0000` 轉到 `9999` 共 10000 種組合都試過，必定會找出密碼，把所有可能都枚舉過一遍，遍是暴力演算法。
+ 暴力演算法可以應用於很多問題，包含數論、樹、圖論等等，而暴力演算法的重點在於**枚舉所有可能**，以樹來說就是樹的遍歷。
+ 舉例來說：
    + [Leetcode 1. Two Sum](https://leetcode.com/problems/two-sum/)  
    給定一個數列，找數列中任兩個數的和為 `target`，回傳兩個數的索引值。
        + 在還沒有認識任何資料結構之前，我們能想到最簡單的方法就是遍歷整個數列，用兩個指標 `i` 與 `j`，各指向一個數，將所有可能檢查過一遍，直到找到目標。
        ```C++
        vector<int> twoSum(vector<int>& nums, int target) {
            for (int i = 0; i < nums.size() - 1; i++)
                for (int j = i + 1; j < nums.size(); j++)
                    if (nums[i] + nums[j] == target)
                        return {i, j};
            return {-1, -1};
        }
        ```
    + 以上例來說，用暴力破解法求解時，求兩數和的時候，我們需進行兩個維度的 for-loop 迴圈來求解。若進一步到三數和、四數和、五數和時，我們會發現，維度會隨著多少個數字和增加。也就是三數和為 3 個迴圈，四數和為 4 個迴圈，以此類推。
    + 以 [複雜度分析 Algorithmic complexity / Big-O / Asymptotic analysis](/cs/algo/bigo)來分析，也就相當於 `k` 數和的時間複雜度為 \\(O(n^k)\\)，這個增長是相當恐怖的。

+ 暴力演算法的特點
    + 簡單粗暴
        + 將所有可能枚舉出來，藉由電腦的運算力高於人腦的特性。
    + 執行效率低
        + 由於所有的情形都需列舉出來，所以執行效率低。
        + 只適用於規模小的問題。
    + 可作用衡量效率問題的基礎算法
        + 暴力法可以看成是某問題時間效能的底限，所以可以用來衡量其它演算法的效率。
---
## 二、暴力演算法應用
### 1. 數組
+ **線性搜索法(Linear Search)**
    + 將一個資料集合的所有元素遍歷一次，找到所需的目標。
    + 例：有一個數列共有 `n` 個元素，找數列中是否含有某數 `target`，若有則回傳其索引值，若無則回傳 `-1`。
    ```C++
    int findTarget(vector<int>& nums, int target) {
        for (int i = 0; i < nums.size(); i++) {
            if (nums[i] == target) return i;
        }
        return -1;
    }
    ```
### 2. 樹
![tree](https://res.cloudinary.com/practicaldev/image/fetch/s---f65OlYQ--/c_imagga_scale,f_auto,fl_progressive,h_420,q_auto,w_1000/https://dev-to-uploads.s3.amazonaws.com/i/e2ru41fjhqs4ombbcedf.png)
+ **深度優先搜索法(Depth-First Search, DFS)**
    + 一直往樹的子節點搜索，直到子葉再退回。
    + 相關的遍歷法有：`pre-ordered traversal(前序遍歷)`、`post-ordered traversal(後序遍歷)`、`in-ordered traversal(中序遍歷)`。
    ```C++
    bool find(TreeNode* root, int target) {
        if (!root) return false;
        if (root->val == target) return true;
        return find(root->left, target) || find(root->right, target);
    }
    ```
+ **廣度優先搜索法(Breadth-First Search, BFS)**
    + 從樹的一根點出發，將此根節點所有的子節點都遍歷完在進行下一次的搜索。
    + 相關的遍歷法有：`level-ordered traversal(層序遍歷)`。
    ```C++
    bool find(TreeNode* root, int target) {
        queue<TreeNode*> q;
        q.push(root);
        while (!q.empty()) {
            int sz = q.size();
            while (sz--) {
                TreeNode* curr = q.front();
                q.pop();
                if (curr->val == target) return true;
                if (curr->left) q.push(curr->left);
                if (curr->right) q.push(curr->right);
            }
        }
        return false;
    }
    ```
### 3. 圖論
![graph](https://www.researchgate.net/profile/Sandeep_Udmale/publication/326749335/figure/download/fig1/AS:952639084838913@1604138265339/a-Breadth-first-search-b-Depth-first-search.png)
+ 圖跟樹的差別在於
    1. 圖可能有多個子節點。
    2. 圖可能是單向或雙向的。
+ 以 DFS, BFS 處理圖論，需要額外處理**已經遍歷過的節點**。
+ **深度優先搜索法(Depth-First Search, DFS)**
    ```C++
    unordered_set<Node*> visited;
    bool find(Node* root, int target) {
        if (!root) return false;
        if (visited.count(root)) return false;
        visited.insert(root);
        if (root->val == target) return true;
        for (TreeNode* child : root->children) {
            if (find(child, target)) return true;
        }
        return false;
    }
    ```
+ **廣度優先搜索法(Breadth-First Search, BFS)**
    ```C++
    unordered_set<Node*> visited
    bool find(Node* root, int target) {
        queue<TreeNode*> q;
        q.push(root);
        visited.insert(root);
        while (!q.empty()) {
            int sz = q.size();
            while (sz--) {
                TreeNode* curr = q.front();
                q.pop();
                if (curr->val == target) return true;
                for (TreeNode* child : root->children) {
                    if (visited.count(child)) continue;
                    q.push(child);
                    visited.insert(child);

                }
            }
        }
        return false;
    }
    ```
## 三、例題
+ 最後用一題 Leetcode 的密碼鎖問題來作結。
+ [Leetcode 752. Open the Lock](https://leetcode.com/problems/open-the-lock/)  
+ 這題密碼鎖多加了一些條件，當轉到某些暗鎖時，會鎖死，所以必須要避開這些數字組合。
+ 這題可以套用圖論的 `bfs`，把暗鎖放入到已經遍歷過的組合 `visited`，接著就是模擬轉動密碼的動作，即可解題。
```C++
string plus(string s, int i) {
    if (s[i] == '9') {
        s[i] = '0';
    } else {
        s[i]++;
    }
    return s;
}
string minus(string s, int i) {
    if (s[i] == '0') {
        s[i] = '9';
    } else {
        s[i]--;
    }
    return s;
}
int openLock(vector<string>& deadends, string target) {
    unordered_set<string> set(deadends.begin(), deadends.end());
    string start = "0000";
    if (set.count(start)) return -1;
    queue<string> q;
    q.push(start);
    int step = 0;
    while (!q.empty()) {
        int sz = q.size();
        while (sz--) {
            string curr = q.front();
            q.pop();
            if (curr == target) return step;
            for (int i = 0; i < 4; i++) {
                string next;
                next = plus(curr, i);
                if (!set.count(next)) q.push(next);
                set.insert(next);
                next = minus(curr, i);
                if (!set.count(next)) q.push(next);
                set.insert(next);
            }   
        }
        step++;
    }

    return -1;
}
```
---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
+ 接著閱讀：[[Algo] 2-2. 貪心演算法](/cs/algo/greedy)