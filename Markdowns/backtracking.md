---
title: "[Algo] 2-4. 回溯法 Backtracking"
keywords: ["C++", "Leetcode", "Algorithm", "Backtracking", "回溯法"]
description: "演算法設計，介紹什麼是回溯法，並示範運用回溯法的思維解題"
date: 2023-01-27T10:50:26+08:00
tags: ["CS", "Algo"]
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
cover:
    image: "/images/faang.webp"
    alt: "Oh! You closed up the window, so you cannot see raining"
    relative: false
    hidden: false
---
## 一、回溯法
+ 回溯法與 `dfs` 相當類似，本質上都是暴力窮舉的演算法，但細微的差異在於：
    + `dfs` 在遍歷**節點**。
    + `backtracking` 在遍歷**樹枝**。
+ 站在回溯樹上的一個節點，需要考慮的只有三件事情：
    1. 路徑
    2. 選擇
    3. 終止條件
+ 以[全排列問題([Leetcode] 46. permutation)](https://leetcode.com/problems/permutations/)來舉例
    ![permutation tree](https://i.stack.imgur.com/9qEB6.jpg)
    + 全排列問題即給定一組數組 `nums`，需返回這些數字的所有排列組合，舉例來說，給定一個數組 `nums = [1,2,3]`，那麼它可能的排列會有：
        + `[1,2,3]`
        + `[1,3,2]`
        + `[2,1,3]`
        + `[2,3,1]`
        + `[3,1,2]`
        + `[3,2,1]`
    + 對應上圖的回溯樹來看，我們在每個樹的節點，都會面臨一次決策，站在樹的根時，相當於我們要選擇排列的第一位，而我們有三個**選擇**，即 `1` 或 `2` 或 `3`。若我們的第一位選擇了 `1`，代表我們選擇了 \\(\text{x}_1=1\\) 的**路徑**，故接下來我們的選擇只剩下兩個，即 `2` 或 `3`。當我們繼續往下做，直到子葉節點時，代表我們已經沒有選擇可選，此時就是我們的**終止條件**。
    + 回憶我們在二叉樹中練習過[前序、中序、後序](/cs/algo/binary_tree/#二前序中序後序)的思維，前序與後序代表我們在遍歷節點**前**與**後**的時間點，而在回溯法，這兩個時間點，各自代表了
        + **將選擇加入路徑**
        + **從路徑中撤銷選擇**
        ![order_traversal](https://labuladong.github.io/algo/images/backtracking/4.jpg)
        ![backtrack_option](https://labuladong.github.io/algo/images/backtracking/5.jpg)
        + 用二叉樹程式碼來說明即：
        ```Cpp
        void traverse(TreeNode* root){
            if (!root) return;
            // preorder: do option
            traverse(root->left);
            traverse(root->right);
            // postorder: retrieve option
        }
        ```
        + N-ary 樹：
        ```Cpp
        class Node{
            int val;
            vector<Node*> children;
        };
        void traverse(Node* root){
            if (!root) return;
            for (Node* child : root->children) {
                // preorder: do option
                traverse(child);
                // postorder: retrieve option
            }
        }
        ```
## 二、回溯法的框架
+ 藉由上面的思維練習，我們可以拼湊出回溯法的基本框架：
```Cpp
vector<Node*> path;
vector<vector<Node*>> res;
void backtrack(Node* root) {
    if (terminate_condition) {  // 當終止條件時
        res.push_back(path);    // 將路徑加入結果
        return;                 // 治原路徑返回
    }
    for (auto& next : root->children) {
        path.push_back(next);   // 將選擇加入路徑
        backtrack(next);
        path.pop_back();        // 從路徑中撤銷選擇
    }
}
```
+ 試著解題[[Leetcode] 46. permutation](https://leetcode.com/problems/permutations/)：
```Cpp
void backtrack(vector<int>& nums, vector<bool>& visited, vector<int>& path) {
    if (path.size() == nums.size()) {
        res.push_back(path);
        return;
    }
    for (int i = 0; i < nums.size(); i++) {
        if (visited[i]) continue;
        visited[i] = true;
        path.push_back(nums[i]);
        backtrack(nums, visited, path);
        visited[i] = false;
        path.pop_back();
    }
}
```
## 三、例題
### 1. [[Leetcode] 51. N-Queens](https://leetcode.com/problems/n-queens/)
+ 經典的 N-Queen 問題，在一個 N x N 的棋盤上，每個橫排、直排與斜線都不能出現 2 個以上的皇后，試求有幾種皇后的排法。
![nqueen](https://assets.leetcode.com/uploads/2020/11/13/queens.jpg)
+ 此題就可以用到回溯法，以 4 x 4 的棋盤為例，我們會建構一個深度為 16 的決策樹：
    + **路徑**：之前做過的選擇
    + **選擇**：選擇要放置皇后，或是不要放置皇后
    + **終止條件**：16 個棋格都走完(4列都走完)
    + 注意：因為在第 `i` 列放了皇后，則同列的其它格子就不能放皇后了，故我們可以直接往第 `i+1` 列前進。故到了第 `n` 列，代表達到終止條件。
+ 程式碼：
```Cpp
int sz;
vector<vector<string>> solveNQueens(int n) {
    sz = n;
    vector<vector<string>> res;
    vector<string> board(n, string(n, '.'));
    backtrack(board, 0, res);
    return res;
}

void backtrack(vector<string>& board, int row, vector<vector<string>>& res){
    if (row == sz){         // 終止條件：走完 n 行
        res.push_back(board);
        return;
    }
    for (int col = 0; col < sz; col++){
        if (!isValid(board, row, col)) continue;
        board[row][col] = 'Q';      // 放皇后
        backtrack(board, row+1, res);
        board[row][col] = '.';      // 撤銷皇后
    }
}

// 直行、橫列、斜線都不能出線皇后
bool isValid(vector<string>& board, int& row, int& col){
    if (row == sz) return true;
    for (int i = row - 1; i >= 0; i--)
        if (board[i][col] == 'Q') return false;
    for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--){
        if (board[i][j] == 'Q') return false;
    }
    for (int i = row - 1, j = col + 1; i >= 0 && j < sz; i--, j++){
        if (board[i][j] == 'Q') return false;
    }
    return true;
}
```

### 2. [[Leetcode] 797. All Paths From Source to Target](https://leetcode.com/problems/all-paths-from-source-to-target/)
+ 給定一個 DAG(directed acyclic graph)，各用 `0` 到 `n-1` 的數字標示，找出所以可能從 `0` 走到 `n-1` 的路徑。其中 `graph[i]` 代表從 `i` 可以到達的下一個節點。
![dag](https://assets.leetcode.com/uploads/2020/09/28/all_1.jpg)
```Cpp
vector<vector<int>> allPathsSourceTarget(vector<vector<int>>& graph) {
    vector<vector<int>> res;
    vector<int> path;
    path.push_back(0);      // 站在起點 0
    backtrack(graph, res, path, 0, -1);
    return res;
}
void backtrack(vector<vector<int>>& graph, vector<vector<int>>& res, vector<int>& path, int curr, int last) {
    if (curr == graph.size()-1) {       // 到達終點 n-1
        res.push_back(path);
        return;
    }
    for (const auto& next : graph[curr]) {
        // if (last == next) continue;      // 若是 directed 或是 cyclic graph，需要避免走回頭路
        path.push_back(next);               // 做選擇
        backtrack(graph, res, path, next, curr);
        path.pop_back();                    // 做撤銷
    }
}
```

### 3. [[Leetcode] 980. Unique Path III](https://leetcode.com/problems/unique-paths-iii/)
+ 機器人必須走過除了牆外的所有棋格，必且到達指定的位置，試求機器人有幾種走法。其中
    + `1` 代表起點。
    + `2` 代表終點。
    + `0` 代表空白棋格，即機器人必須要經過的棋格。
    + `-1` 代表牆，即機器人無須經過且不能經過的棋格。
+ 注意此題的選擇、與撤銷的位置與框架中的前序、後序位置不同，試想會有什麼效果
![unique_path_iii](https://assets.leetcode.com/uploads/2021/08/02/lc-unique1.jpg)
```Cpp
int res;
int uniquePathsIII(vector<vector<int>>& grid) {
    res = 0;
    int m = grid.size(), n = grid[0].size();
    // 先記錄機器人的起點與終點
    pair<int,int> start, end;
    // 記錄機器人所需走多少步(選擇的次數): 棋格數-障礙-1
    int left = m*n;
    for (int i = 0; i < m; i++) {
        for (int j = 0; j < n; j++) {
            if (grid[i][j] == 1) {
                start = {i, j};
                left--;
            } else if (grid[i][j] == -1) {
                left--;
            }
        }
    }
    backtrack(grid, start.first, start.second, left);
    return res;
}
int dir[4][2] = {{1,0},{0,1},{-1,0},{0,-1}};
void backtrack(vector<vector<int>>& grid, int row, int col, int left) {
    // 超出棋格、或是已經走過
    if (row < 0 || row >= grid.size() || col < 0 || col >= grid[0].size() || grid[row][col] == -1) return;
    // 終止條件：到達目標且每一個空白棋格都走完(left == 0)
    if (grid[row][col] == 2 && left == 0) {
        res++;
        return;
    }
    int tmp = grid[row][col];       // 做選擇
    grid[row][col] = -1;            // 做標記，代表已走過
    for (const auto& d : dir) {
        backtrack(grid, row+d[0], col+d[1], left-1);
    }
    grid[row][col] = tmp;           // 做撤銷
}
```
---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
+ 想要複習：[[Algo] 2-3. 分治法](/cs/algo/divice_and_conquer)
+ 接著閱讀：[[Algo] 2-5. 動態規劃](/cs/algo/dp)