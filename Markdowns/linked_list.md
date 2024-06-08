---
title: "[Algo] 0-3. 鏈表(Linked List)"
keywords: ["C++", "Leetcode", "Algorithm", "linked_list", "鏈表", "list", "ListNode"]
description: "演算法結構介紹，介紹鏈表的遍歷，與常見的經典考題與常用到的操作手法"
date: 2022-10-06T22:30:28+08:00
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
## 一、鏈表的基本結構
+ 鏈表是由節點和指針構成的數據結構，每個節點存有一個值，和一個指向下一個節點的指針。不同於數組，鏈表並不能隨機訪問，必須透過指針找到該節點才能獲取其值；同理在未遍歷到鏈表結尾時，我們也無法知道鏈表長度，除非依賴其它數據結構儲存長度。
+ LeetCode 中默認的鏈表：
```C++
struct ListNode {
    int val;
    ListNode *next;
    ListNode(int x) : val(x), next(nullptr) {}
};
```
## 二、鏈表的基本操作
+ 在開始演算法實踐前，先來練習一下鏈表的 CRUD 吧！
### 1. 查(Read)
+ 由於鏈表並非在儲存格中連續分布，所以無法用索引進行隨機訪問，所以我們必須逐個訪問，直到到達我們想要的元素。
+ 藉由指針每次指向當前節點的 next，移動 n 次到達 index 為 n 的節點。
```C++
int at(ListNode* head, int n){      // index 為 n
    ListNode* curr = head;
    while (n--){        // 移動 n 次
        curr = curr->next;
    }
    return curr->val;
}
```
### 2. 改(Update)
+ 改的步驟，只是將查完的元素予以賦值。
```C++
void update(ListNode* head, int n, int val){
    ListNode* curr = head;
    while (n--){
        curr = curr->next;
    }
    curr->val = val;      // 查完後賦值
}
```
### 3. 增(create)
+ 如果要新增節點，則必須找到要插入位置的前一個節點。
```C++
ListNode* insert(ListNode* head, int n, int val){
    if (!head) return NULL;                             // 處理當鏈表為空的狀態
    if (n == 0){                                        // 處理當插入位置為 0 時的特例
        ListNode* newHead = new ListNode(val, head);
        head = newHead;
        return head;
    }
    ListNode* curr = head;
    while (curr && --n){                                // 移動到插入位置的前一位
        curr = curr->next;
    }
    ListNode* tmp = curr->next;                         // 預先存下來插入位置的後一位
    curr->next = new ListNode(val);                     // 插入元素
    curr->next->next = tmp;                             // 將元素的下一位指派給存下來的後一位
    return head;
}
```
+ 上述的寫法很直觀，但需要處例首位的特例，不夠漂亮，這時我們常會用到 **DUMMY HEAD** 的手法，即在鏈表最開頭先創一個假的節點，最後再返回該節點的下一位。
```C++
ListNode* insert(ListNode* head, int n, int val){
    if (!head) return NULL;
    ListNode* dummy = new ListNode(-1, head);           // 創建一個 dummy head
    ListNode* curr = dummy;
    while (curr && n--){                                // 注意為 n-- 
        curr = curr->next; 
    }
    ListNode* tmp = curr->next;
    curr->next = new ListNode(val);
    curr->next->next = tmp;
    return dummy->next;                                 // 注意為返回 dummy 的下一位
}
```
+ 前面兩解都是在可以取得前一位，且不更改節點的值的前提下的解法。如果只能取得當下的節點，則可以使用**增+更**的手法。
```C++
void insert(ListNode* node, int val){
    int tmp = node->val;
    node->val = val;
    node->next = new ListNode(tmp, node->next);
}
```
### 4. 減(delete)
+ 與增類似，取得前一位再利用更改指針的方式達到刪除。
![delete](https://i.imgur.com/Qof9hCw.png)
```C++
ListNode* erase(ListNode* head, int n, int val){
    if (!head) return NULL;
    ListNode* dummy = new ListNode(-1, head);
    ListNode* curr = dummy;
    while (curr && n--){
        curr = curr->next; 
    }
    curr->next = curr->next->next;      // 將前一位的後一位指給後一位
    return dummy->next;
}
```
+ 只能取得當下節點的作法：
#### [[LeetCode. 237] Delete Node in a Linked List(Medium)](https://leetcode.com/problems/delete-node-in-a-linked-list/)
```C++
void insert(ListNode* node, int val){
    node->val = node->next->val;        // 將當前的值賦值成下一位的值
    node->next = node->next->next;      // 將下一個節點刪除
}
```
## 三、鏈表的進階操作
### 1. 刪值
+ 如何刪除表中第一個出現指定值的節點，同樣找到該值的前一個節點，再使用刪除的手法即可：
```C++
void remove(ListNode* head, int target){
    ListNode* prev = NULL;
    ListNode* curr = head;
    while (curr && curr->val != target){
        prev = curr;
        curr = curr->next;
    }
    if (!prev)                  // 處理例外
        head = head->next;
    else
        prev->next = prev->next->next;
}
```
### 2. 建表
+ 如何藉由一個數組建立一鏈表，可以藉由前面使用的 dummy head 的手法：
```C++
ListNode* build(vector<int> nums){
    ListNode* dummy = new ListNode(-1);
    ListNode* prev = dummy;
    ListNode* curr = NULL;

    for (int i = 0; i < nums.size(); i++){
        curr = new ListNode(nums[i]);
        prev->next = curr;
        prev = curr;
        curr = curr->next;
    }
    ListNode* head = dummy->next;
    delete(dummy);
    return head;
}
```
### 3. 鏈表的後序遍歷
+ 前文說過，鏈表也可以做前序與後序的遍歷，在此我們再複習一次。
```C++
void traverse(ListNode* head){
    // pre-order
    traverse(head->next);
    // post-order
}
```
+ 如果我們想移除鏈表中所有值等於 target 的節點，用迭代的作法為：
```C++
void removeAll(ListNode* head, int target){
    ListNode* dummy = new ListNode(-1, head);
    ListNode* curr = dummy;
    while (curr && curr->next){
        if (curr->next->val == target){         // 當前節點的下一位符合 target 則刪除它
            curr->next = curr->next->next;
        } else {
            curr = curr->next;                  // 否則則繼續後下遍歷
        }
    }
}
```
+ 那如果用遞迴的寫法呢：
```C++
void removeAll(ListNode* head, int target){
    if (!head) return;                          // 假如鏈表為空，則退出函式
    while (head && head->val == target){
        if (head->next){
            head->val = head->next->next;
            head->next = head->next->next;      // 刪除
        } else {
            head = NULL;                        // 當最後一個元素需移除時
        }
    }
    removeAll(head->next, target)               // 前序遍歷
}
```
+ 後序跟前序有個很維妙的差別在於：**後序遍歷可以將值傳回給前一個元素**！
+ 試試看用後序回傳值的特徵來實作這個函式
```C++
void removeAll(ListNode* head, int target){
    int tmp;
    if (head->next) 
        tmp = removeAll(head->next, target)     // recursion
    if (tmp == target)                          // 後序遍歷可以取得下一位的回傳的值       
        head->next = head->next->next;          // 有了需要刪除的節點的前一位，要刪除就容易啦！
    return head->val                            // 傳回當前節點的值
}
```
## 四、秀一波的操作
### 1. 刪值
+ 用兩個節點去做到鏈表刪除的操作，還是有一點點不夠美，試試看下面這個 pointer to pointer 的解法吧！
+ 改自文章[你所不知道的 C 語言: linked list 和非連續記憶體](https://hackmd.io/@sysprog/c-linked-list?fbclid=IwAR2179AHDYjsbYnbDdmCyiw7d3CF0yjehNGU-GcDsNAeZ_CkermprcDBzlo)
![linus delete](https://hackmd.io/_uploads/HyuZ6Z13t.png)
```C++
void remove(ListNode* head, int target){
    ListNode** curr = &head;    // 將指向指針的 curr 指向 head
    while ((*curr)->val != target)
        curr = &(*curr)->next;
    if (!(*curr)) return;       // 避免掉指向 NULL
    *curr = (*curr)->next
}
```
### 2. 建表
+ 學會上面這個 pointer to pointer 的作法，不如試試看來用這個方法來建表！
```C++
ListNode* build(vector<int> nums){
    ListNode* head = new ListNode(nums[0]);
    ListNode** curr = &head;
    for (int i = 0; i < nums.size(); i++){
        (*curr)->next = new ListNode(nums[i]);
        curr = &(*curr)->next;
    }
}
```
## 五、鏈表的演算法
### 1. 反轉鏈表
#### [[LeetCode. 206] Reverse Linked List(Easy)](https://leetcode.com/problems/reverse-linked-list/)
+ 藉由剛剛學習到鏈表的操作，用迭代的方式來解題吧。
+ 考慮到一個反轉鏈表的連續操作，我們需要有三個節點 prev, curr, next。
```C++
ListNode* reverse(ListNode* head){
    ListNode* prev = NULL;
    ListNode* curr = head;
    ListNode* next = NULL;
    while (curr){
        next = curr->next;      // 先記住下一個位置
        curr->next = prev;      // 將指針指向前一位，以達成反轉的目的
        prev = curr;            // 往前移動
        curr = next;            // 往前移垂
    }
    return prev;
}
```
+ 那如果用遞迴的方式寫呢？
![reverselist2](https://labuladong.github.io/algo/images/%e5%8f%8d%e8%bd%ac%e9%93%be%e8%a1%a8/2.jpg)
![reverselist3](https://labuladong.github.io/algo/images/%e5%8f%8d%e8%bd%ac%e9%93%be%e8%a1%a8/3.jpg)
![reverselist4](https://labuladong.github.io/algo/images/%e5%8f%8d%e8%bd%ac%e9%93%be%e8%a1%a8/5.jpg)
```C++
ListNode* reverse(ListNode* head){
    if (!head || head->next) return head;   // 處理終止條件
    ListNode last = reverse(head->next);    // post-order traversal：回傳已排序好的子鏈表，並傳回最後一項
    head->next->next = head;
    head->next = NULL;
    return last;
}
```
#### [[Followup] 反轉前 N 個節點
+ 反轉鏈表的前 N 個節點：用前面的函式稍作修改
![reverselistN](https://labuladong.github.io/algo/images/%e5%8f%8d%e8%bd%ac%e9%93%be%e8%a1%a8/7.jpg)
```C++
ListNode* successor = NULL;
ListNode* reverseN(ListNode* head, int n){
    if (n == 1){                            // 只反轉 1 個節點相當於沒有反轉，故轉回自己
        successor = head->next;             // 記錄反轉後的鏈表要接到哪裡->剩餘鏈表的頭
        return head;
    }
    ListNode last = reverseN(head->next, n-1);
    head->next->next = head;
    head->next = successor;                 // 最後將鏈表的尾巴接到剩餘鏈表的頭
    return last;
}
```
#### [[LeetCode. 92] Reverse Linked List II(Medium)](https://leetcode.com/problems/reverse-linked-list-ii/)
+ 反轉第 m 到第 n 個節點中間的節點 
+ 前進 m - 1 次就相當於就相當於反轉前 (n-m-1) 個節點，就可以用 reverseN 解了。
```C++
ListNode* reverseBetween(ListNode* head, int m, int n){
    if (m == 1){
        return reverseN(head, n);                       // 與 LeetCode.92 一樣
    }
    head->next = reverseBetween(head->next, m-1, n-1);  // 前進到 base case
    return head;
}
```
#### [[LeetCode. 25] Reverse Nodes in k-Group (Hard)](https://leetcode.com/problems/reverse-nodes-in-k-group/)
+ 結合前面的經驗，注意**遞迴該返回的值是什麼**。
+ 注意結尾若節數小於 k 則不則 reverse。
```C++
ListNode* reverseKGroup(ListNode* head, int k) {
    ListNode* curr = head;
    int cnt = 0;
    while (curr && cnt < k){
        curr = curr->next;
        cnt++;
    }
    if (cnt == k){              // 當節數小於 k 時，不做 reverse
        curr = reverseKGroup(curr, k);  // 傳回的是 reverse 完的鏈表的 head，故需把 reverse 完的尾與之相接
        while (cnt-- > 0){
            ListNode* next = head->next;
            head->next = curr;
            curr = head;
            head = next;
        }
        return curr;            // 當節數等於 k 時回傳的是尾巴
    }
    return head;                // 注意節數小於 k 時仍回傳 head
}
```

---

### 2. 環型鏈表(龜兔賽跑-快慢指針)
#### [[LeetCode. 141] Linked List Cycle(Easy)](https://leetcode.com/problems/linked-list-cycle/)
+ 快慢指針是雙指針的一種應用，利用兩個指針移動的速度不同來達到目的。最經典的題型就是找尋鏈表是否含有環。
+ 要檢查鏈表是否有環，可以使用找尋圖(graph)中是否有環的技巧，並利用 visited 來檢查是否有拜訪過，但下面快慢指針的技巧可以不用額外使用空間，使空間複雜度降到 \\(O(1)\\)。
```C++
bool hasCycle(ListNode *head) {
    ListNode* fast = head;
    ListNode* slow = head;
    while (fast && fast->next){             // 確保快指針與他的下一位都不是 NULL
        fast = fast->next->next;            // 快指針走兩步
        slow = slow->next;                  // 慢指針走一步
        if (fast == slow) return true;      // 若兩者相撞，則必有環
    }
    return false;
}
```
#### [[LeetCode. 142] Linked List Cycle II(Medium)](https://leetcode.com/problems/linked-list-cycle-ii/)
+ 此題是要找尋鏈表中若有環，則相交點是哪一點：
+ 因為快指針走的距離是慢指針 k 的兩倍，令相遇點距相交點距離為 m 圓環的長度為 L：  
    \\(\text{L + m + k = 2 * k}\\)  
    \\(\text{L = k - m}\\)  
    故起點到相交點的長度 \\(\text{k - m}\\) 與相遇點到相交點的長度 \\(\text{k - m}\\) 相同。
![cyclic linkedlist](https://labuladong.github.io/algo/images/%e5%8f%8c%e6%8c%87%e9%92%88/2.jpeg)
```C++
ListNode *detectCycle(ListNode *head) {
    ListNode* fast = head;
    ListNode* slow = head;
    while (fast && fast->next){
        fast = fast->next->next;
        slow = slow->next;
        if (fast == slow) break;                // 若有環則退出
    }   
    if (!fast || !fast->next) return NULL;      // 若快指針已經走到底表示沒有環
    fast = head;                              // 讓其中一個指針從頭開始走，並一同樣的速度走
    while (fast != slow){                     // 相遇點即為相交點
        fast = fast->next;
        slow = slow->next;
    }
    return fast;
}
```
#### [[LeetCode. 876] Middle of the Linked List(Easy)](https://leetcode.com/problems/middle-of-the-linked-list/)
+ 這題當然可以先遍歷一遍取得鏈表長度後，再重新以長度計量，走一半的長度來得到答案，但很顯然不夠漂亮，用快慢指針，令快指針比慢指針移動速度快兩倍，當快指針走完時，慢指針即指向中點。以此類推可求1/3的節點、2/5的節點等。
```C++
ListNode* middleNode(ListNode* head) {
    ListNode* fast = head;
    ListNode* slow = head;
    while (fast && fast->next){
        fast = fast->next->next;
        slow = slow->next;
    }   
    return slow;
}
```
### 3. 雙指針(前後指針)
#### [[LeetCode. 19] Remove Nth Node From End of List(Medium)](https://leetcode.com/problems/remove-nth-node-from-end-of-list/)
+ 這題也是簡單的雙指針問題，當前指針先走 n 步，兩指針以同樣速度往前走(即前後指針始終保持 n 的距離)，則前指針走完時，後指針指向倒數第 k 個節點。
```C++
ListNode* removeNthFromEnd(ListNode* head, int n) {
    // 注意以下我們要刪除第 n 個節點，故我們需找第 n-1 個節點，為避免刪除第一個節點的例子，我們引入 dummy
    ListNode* dummy = new ListNode(-1, head);  
    ListNode* slow = dummy;
    ListNode* fast = dummy;
    while (fast && n--){                // 前指針先行走 n 個節點
        fast = fast->next;
    }
    while (fast->next){                 // 保持等速
        slow = slow->next;
        fast = fast->next;
    }
    slow->next = slow->next->next;      // 刪除第 n 個節點

    return dummy->next;
}
```
#### [[LeetCode. 160] Intersection of Two Linked Lists(Easy)](https://leetcode.com/problems/intersection-of-two-linked-lists/)
+ 找兩條鏈表的相交點，這題也可以用雙指針的方式解，當 A 鏈懷走完鏈表立即讓它接回 B 鏈表，B 鏈表亦如是，則相遇點則會是相交點，因為此時它們各別則的距離是都是 A 鏈表的長度加上 B 鏈表的長度，但要注意要記錄是否已經接過一遍，如果沒有相交點，又無限接下去，則程式永遠不會停止。
![intersection1](https://labuladong.github.io/algo/images/%e9%93%be%e8%a1%a8%e6%8a%80%e5%b7%a7/4.png)
![intersection2](https://labuladong.github.io/algo/images/%e9%93%be%e8%a1%a8%e6%8a%80%e5%b7%a7/5.jpeg)
![interscetion3](https://labuladong.github.io/algo/images/%e9%93%be%e8%a1%a8%e6%8a%80%e5%b7%a7/6.jpeg)

```C++
ListNode *getIntersectionNode(ListNode *headA, ListNode *headB) {
    ListNode* a = headA;
    ListNode* b = headB;
    bool flagA = true;          // 用來標記是否已接過另一鏈表
    bool flagB = true;          // 用來標記是否已接過另一鏈表
    while (a && b){
        if (a == b) return a;   // 相遇表示相交點
        a = a->next;
        b = b->next;
        if (!a && flagA){
            a = headB;
            flagA = false;      // 已接過另一鏈表
        } 
        if (!b && flagB){
            b = headA;
            flagB = false;      // 已接過另一鏈表
        } 
    }
    return NULL;
}
```
#### [[LeetCode. 86] Partition List(Medium)](https://leetcode.com/problems/partition-list/)
+ 具體作法可將鏈表一分為二，待兩條鏈表皆完成後再頭尾相接。
```C++
ListNode* partition(ListNode* head, int x) {
    ListNode* dummy1 = new ListNode(-1);
    ListNode* dummy2 = new ListNode(-1);
    ListNode* curr1 = dummy1;
    ListNode* curr2 = dummy2;
    while (head){
        if (head->val < x){
            curr1->next = head;
            curr1 = curr1->next;
        } else {
            curr2->next = head;
            curr2 = curr2->next;
        }
        head = head->next;
    }
    curr1->next = dummy2->next;
    curr2->next = NULL;
    return dummy1->next;
}
```
#### [[LeetCode. 21] Merge Two Sorted Lists(Easy)](https://leetcode.com/problems/merge-two-sorted-lists/)
+ 簡單的 if-else，搭配 dummy 的做法即可解題。
```C++
ListNode* mergeTwoLists(ListNode* list1, ListNode* list2) {
    ListNode* dummy = new ListNode(-1);
    ListNode* curr = dummy;
    while (list1 && list2){
        if (list1->val <= list2->val){
            curr->next = list1;
            list1 = list1->next;
        } else {
            curr->next = list2;
            list2 = list2->next;
        }
        curr = curr->next;
    }
    curr->next = list1 ? list1 : list2;
    return dummy->next;
}
```
### 4. 優先佇列
#### [[LeetCode. 23] Merge k Sorted Lists(Hard)](https://leetcode.com/problems/merge-k-sorted-lists/)
+ 這一題有點 tricky，我們可以用到優先佇列，由於每次比較只會比較鏈表的頭節表，故我們連續將鏈表推至 min heap 上，並每次把 min heap 頂端的節點接到新的鏈表後，再把 min heap 上的鏈表拿去頭後，再丟回優先佇列中，至到鏈表走完，即完成。
```C++
ListNode* mergeKLists(vector<ListNode*>& lists) {
    auto cmp = [](ListNode* a, ListNode* b){return a->val > b->val;};
    priority_queue<ListNode*, vector<ListNode*>, decltype(cmp)> pq(cmp);
    ListNode* dummy = new ListNode(-1);
    ListNode* curr = dummy;
    for (ListNode* node : lists){
        if (node) pq.push(node);
    }
    while (!pq.empty()){
        ListNode* node = pq.top();
        pq.pop();
        curr->next = node;
        curr = curr->next;
        node = node->next;
        if (node) pq.push(node);
    }
    return dummy->next;
}
```
---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
+ 想要複習：[[Algo] 0-2. 演算法思維](/cs/algo/concept)
+ 接著閱讀：[[Algo] 0-4. 二元樹(Binary Tree)](/cs/algo/binary_tree)