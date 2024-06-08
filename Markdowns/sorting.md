---
title: "[Algo] 3-0. Sorting"
keywords: ["C++", "Leetcode", "Algorithm", "sorting"]
description: "從排序開始，練習將想法實踐，All start from sorting"
date: 2023-03-16T19:50:21+08:00
tags: ["CS", "Algo", "sorting"]
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
> 前言：  
> 1. 在開始練習各種演算法題型時，最先需要養成的是，如何選用「適當」的演算法，題目往往不會只有一種解，但合適的演算法可以如同走捷徑一般，快速且優雅的達到目標。  
> 2. 在實作程式前，更重要的是，寫下一段 pseudo code，試著說明其複雜度，並觀察是否有冗餘的空間可以優化。  
> 3. 在腦海中模擬一遍程式碼之後，最後才是快速的將程式碼實作出來。  
> 
> 在這一章節，我們將練習如何將「想法」轉換成「實作」。並且我們必須熟悉如何計算其時間複雜度。
## 一、Cheat Table
+ 首先我們需要先瞭解每一種資料結構的各種操作的時間複雜度，以便我們選擇適合的資料結構與演算法。
    + 下面這種表的 `Array`, `Stack`, `Queue`, `Linked List`, `Hash Table`, `Binary Search Tree` 基本上是要背起來的，其餘的遇到再去認識就好。
![bigO](/CS/algo/images/bigO.png)

+ 接下來就輪到練習實作了，排序演算法是個很好的練習，試著把下表中的排序演算法完成，並且計算其時間複雜度吧。
    + 參考題目 [Leetcode 912. Sort an Array](https://leetcode.com/problems/sort-an-array/)
![chartTable](/CS/algo/images/cheatTable.png)


## 二、Sorting Algorithm
### 0. 測資
+ 這個 file 是我寫的測資，可以拿來測試自己的實作，用法是 `#include "agtr.h"`，之後用 `judge()` 函式測試你寫好的 function。
```C++
#include <iostream>
#include <random>
#include <vector>

using namespace std;

class agtr{
public:
    static vector<int> exec(int n, int minv, int maxv) {
        if (minv > maxv) return {};
        else if (minv == maxv) return vector<int>(n, minv);
        vector<int> res;
        random_device rd;
        mt19937 mt(rd());
        uniform_real_distribution<double> dist(minv, maxv);
        while (n--) {
            res.push_back(dist(mt));
        }
        return res;
    }
    static vector<int> exec(int n) {
        return exec(n, 0, 10);
    }

    static vector<int> exec() {
        return exec(10);
    }
    static void print(vector<int>& nums) {
        cout << "[";
        for_each(nums.begin(), nums.end()-1, [](int x) { cout << x << ","; });
        cout << *(nums.end()-1) << "]";
    }
    static bool check(vector<int>& nums, vector<int> copy) {
        sort(copy.begin(), copy.end());
        for (int i = 0; i < nums.size(); i++) {
            if (nums[i] != copy[i]) return false;
        }
        return true;
    }
    static void judge(void (*func)(vector<int>&)) {
        int n = 10;
        bool test = true;
        int cnt = 0;
        while (n--) {
            auto nums = exec();
            auto copy = vector<int>(nums.begin(), nums.end());
            print(nums);
            (*func)(nums);
            cout << "->";
            print(nums);
            int result = check(nums, copy);
            cout << "(" << (result ? "Pass" : "Fail") << ")" << endl;
            if (result) cnt++;
            test &= result;
        }
        if (test) {
            cout << "Pass! (10/10)" << endl;
        } else {
            cout << "Fail! (" << cnt << "/10)" << endl;
        }
    }
};
```
+ 以下為測試的方式
```C++
# include "agtr.h"
void sort(vector<int>& nums) {...}  // 你的實作

int main() {
    agtr::judge(sort);      // 用這個函式測試你的實作
    return 0;
}
```
### 1. Bubble Sort
```C++
void sort(vector<int>& nums) {
    int n = nums.size();
    for (int i = n-1; i > 0; i--) {
        for (int j = 0; j < i; j++) {
            if (nums[j] > nums[j+1]) swap(nums[j], nums[j+1]);
        }
    }
}
```
### 2. Selection SOrt
```C++
void sort(vector<int>& nums) {
    int n = nums.size();
    for (int i = 0; i < n-1; i++) {
        int p = i;
        for (int j = i+1; j < n; j++) {
            if (nums[j] < nums[p]) p = j;
        }
        swap(nums[p], nums[i]);
    }
}
```

### 3. Insertion Sort
```C++
void sort(vector<int>& nums){
    int n = nums.size();
    for (int i = 1; i < n; i++) {
        int j = i-1;
        int curr = nums[i];
        for (; j >= 0; j--) {
            if (nums[j] <= curr) {
                break;
            }
            nums[j+1] = nums[j];
        }
        nums[j+1] = curr;
    }
}
```

### 4. Heap Sort
```C++
void heapify(vector<int>& nums, int i) {
    int left = 2*i+1;
    int right = 2*i+2;
    int p = i;
    int n = nums.size();
    if (left < n && nums[left] < nums[p]) p = left;
    if (right < n && nums[right] < nums[p]) p = right;
    if (p != i) {
        swap(nums[i], nums[p]);
        heapify(nums, p);
    }
}
void sort(vector<int>& nums) {
    vector<int> vec(nums.begin(), nums.end());
    int n = vec.size();
    int parent = (n-1)/2;
    for (int i = parent; i >= 0; i--) {
        heapify(vec, i);
    }

    for (int i = 0; i < n; i++) {
        nums[i] = vec[0];
        vec[0] = vec.back();
        vec.pop_back();
        heapify(vec, 0);
    }
}
```

### 5. Tree Sort
```C++
class TreeNode {
private:
    TreeNode* left, *right;
    int val;
    TreeNode* insert(TreeNode* root, int val) {
        if (!root) {
            root = new TreeNode(val);
            return root;
        }
        if (val < root->val) {
            root->left = insert(root->left, val);
        } else {
            root->right = insert(root->right, val);
        }
        return root;
    }
    void dfs(TreeNode* root, vector<int>& nums) {
        if (!root) return;
        dfs(root->left, nums);
        nums.push_back(root->val);
        dfs(root->right, nums);
    }
public:
    TreeNode() {}
    TreeNode(int val)
        : val(val) {}
    TreeNode(int val, TreeNode* left, TreeNode* right)
        : val(val), left(left), right(right) {}

    void insert(int val) {
        insert(this, val);
    }
    vector<int> getArray() {
        vector<int> nums;
        dfs(this, nums);
        return nums;
    }

};

void sort(vector<int>& nums) {
    TreeNode* root = new TreeNode(nums[0]);
    for (int i = 1; i < nums.size(); i++) {
        root->insert(nums[i]);
    }
    nums = root->getArray();
}
```

### 6. Merge Sort
```C++
void merge(vector<int>& nums, int left, int mid, int right) {
    int i = left, j = mid + 1;
    vector<int> tmp;
    while (i <= mid && j <= right) {
        if (nums[i] < nums[j])
            tmp.push_back(nums[i++]);
        else
            tmp.push_back(nums[j++]);
    }
    while (i <= mid) tmp.push_back(nums[i++]);
    while (j <= right) tmp.push_back(nums[j++]);
    for (i = left; i <= right; i++) {
        nums[i] = tmp[i-left];
    }
}

void sort(vector<int>& nums, int left, int right) {
    if (right <= left) return;
    int mid = left + (right-left)/2;
    sort(nums, left, mid);
    sort(nums, mid+1, right);
    merge(nums, left, mid, right);
}

void sort(vector<int>& nums) {
    sort(nums, 0, nums.size()-1);
}
```

### 7. Quick Sort
```C++
nt partition(vector<int>& nums, int left, int right) {
    int pivot = left;
    int i = left;
    int j = right+1;
    while (true) {
        while (i < right && nums[++i] < nums[pivot]);
        while (j > left && nums[--j] > nums[pivot]);
        if (i >= j) break;
        swap(nums[i], nums[j]);
    }
    swap(nums[pivot], nums[j]);
    return j;
}

void sort(vector<int>& nums, int left, int right) {
    if (left >= right) return;
    int pivot = partition(nums, left, right);
    sort(nums, left, pivot-1);
    sort(nums, pivot+1, right);
}

void sort(vector<int>& nums) {
    sort(nums, 0, nums.size()-1);
}
```

### 8. Tim Sort
```C++
#define MIN_MERGE 32

int minRunLength(int n) {
    int r = 0;
    while (n >= MIN_MERGE) {
        r |= (n & 1);
        n >>= 1;
    }
    return n + r;
}

void insertionSort(vector<int>& nums, int left, int right) {
    int n = nums.size();
    for (int i = left+1; i <= right; i++) {
        int j = i-1;
        int curr = nums[i];
        for (; j >= left; j--) {
            if (nums[j] <= curr) {
                break;
            }
            nums[j+1] = nums[j];
        }
        nums[j+1] = curr;
    }
}

void merge(vector<int>& nums, int left, int mid, int right) {
    int i = left;
    int j = mid + 1;
    vector<int> tmp;
    while (i <= mid && j <= right) {
        if (nums[i] < nums[j])
            tmp.push_back(nums[i++]);
        else
            tmp.push_back(nums[j++]);
    }
    while (i <= mid)
        tmp.push_back(nums[i++]);
    while (j <= right)
        tmp.push_back(nums[j++]);
    for (i = left; i <= right; i++) {
        nums[i] = tmp[i-left];
    }
}

void sort(vector<int>& nums) {
    int minRun = minRunLength(MIN_MERGE);
    int n = nums.size();
    for (int i = 0; i < n; i += minRun) {
        int hi = min((i + MIN_MERGE - 1), n-1);
        insertionSort(nums, i, hi);
    }
    for (int size = minRun; size < n; size <<= 1) {
        for (int left = 0; left < n; left += (size << 1)) {
            int mid = left + size - 1;
            int right = min((left + (size << 1) - 1), n-1);
            if (mid < right) {
                merge(nums, left, mid, right);
            }
        }
    }
}
```

### 9. Shell Sort
```C++
void sort(vector<int>& nums) {
    int n = nums.size();
    for (int gap = n>>1; gap > 0; gap>>=1) {
        for (int i = gap; i < n; i++) {
            int tmp = nums[i];
            int j;
            for (j = i; j >= gap && nums[j-gap] > tmp; j -= gap) {
                nums[j] = nums[j-gap];
            }
            nums[j] = tmp;
        }
    }
}
```

### 10. Counting Sort
```C++
void sort(vector<int>& nums) {
    vector<int> dp(10, 0);
    for (const auto& x : nums) {
        dp[x]++;
    }
    int j = 0;
    for (int i = 0; i < 10; i++) {
        while (dp[i]-- > 0) {
            nums[j++] = i;
        }
    }
}
```

---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
<!-- + 想要複習：[[Algo] 4-1. 暴力演算法](/cs/algo/brute_force) -->
+ 接著閱讀：[[Algo] 3-1. Two Pointer/Sliding Window](/cs/algo/two_pointer)