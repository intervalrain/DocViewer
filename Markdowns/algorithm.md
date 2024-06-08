---
title: "[Algo] 1-9. Algorithm"
keywords: ["C++", "Algorithm", "STL"]
description: "C++ 的內建演算法庫使用與範例"
date: 2023-01-03T21:49:42+08:00
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
# algorithm
`<algorithm>` 定義了專為元素集合設計的函式。  
元素集合包含可以被迭代器或指標存取的一系列元素，例如陣列或 STL container。但且注意，演算法只會透過迭代器去操作容器中的值，並不會更改其結構或是大小。
## 一、函式
### 1. 無修改值的操作
#### all_of
`bool all_of(Iterator first, Iterator last, UnaryPredicate pred)`
+ 檢查是否**全部**的元素都符合判斷式。
```C++
#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int main(){
    vector<int> arr1 = {1,2,3,4,5};
    vector<int> arr2 = {1,3,5,7,9};
    vector<int> arr3 = {2,4,6,8,10};
    auto isodd = [](int x)->bool{ return x%2; };
    cout << all_of(arr1.begin(), arr1.end(), isodd) << endl;    // 0
    cout << all_of(arr2.begin(), arr2.end(), isodd) << endl;    // 1
    cout << all_of(arr3.begin(), arr3.end(), isodd) << endl;    // 0

    return 0;
}
```
---
#### any_of
`bool any_of(Iterator first, Iterator last, Predicate pred)`
+ 檢查是否有**任一**元素符合判斷式。
```C++
#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int main(){
    vector<int> arr1 = {1,2,3,4,5};
    vector<int> arr2 = {1,3,5,7,9};
    vector<int> arr3 = {2,4,6,8,10};
    auto isodd = [](int x)->bool{ return x%2; };
    cout << any_of(arr1.begin(), arr1.end(), isodd) << endl;    // 1
    cout << any_of(arr2.begin(), arr2.end(), isodd) << endl;    // 1
    cout << any_of(arr3.begin(), arr3.end(), isodd) << endl;    // 0

    return 0;
}
```
---
#### none_of
`bool none_of(Iterator first, Iterator last, Predicate pred)`
+ 檢查是否**沒有任何**元素符合判斷式
```C++
#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

int main(){
    vector<int> arr1 = {1,2,3,4,5};
    vector<int> arr2 = {1,3,5,7,9};
    vector<int> arr3 = {2,4,6,8,10};
    auto isodd = [](int x)->bool{ return x%2; };
    cout << none_of(arr1.begin(), arr1.end(), isodd) << endl;    // 0
    cout << none_of(arr2.begin(), arr2.end(), isodd) << endl;    // 0
    cout << none_of(arr3.begin(), arr3.end(), isodd) << endl;    // 1

    return 0;
}
```
---
#### for_each
`void for_each(Iterator first, Iterator last, Function fn)`
+ 用 function *fn* 遍歷範圍 [first,last) 中的元素。
```C++
#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int main(){
    vector<int> arr = {1,2,3,5,8,13,21};
    for_each(arr.begin(), arr.end(), [](int x){ cout << x << " "; });
    // 1 2 3 5 8 13 21

    return 0;
}
```
#### find
`Iterator find(Iterator first, Iterator last, const T& val)`
+ 在範圍中找與指定元素相等的元素，若沒有與之相符的元素則回傳 `last`。
```C++
#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int main(){
    vector<int> arr = {1,3,7,4,9,12,5};
    vector<int>::iterator it1 = find(arr.begin(), arr.end(), 12);
    auto it2 = find(arr.begin(), arr.end(), 8);

    cout << distance(arr.begin(), it1) << endl;     // 5
    cout << distance(arr.begin(), it2) << endl;     // 7


    return 0;
}
```
---
#### find_if
`Iterator find_if(Iterator first, Iterator last, UnaryPredicate pred)`
+ 在範圍中找第一個符合條件的元素，若沒有與之相符的元素則回傳 `last`。
```C++
#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int main(){
    auto isOdd = [](int x)->bool{ return x%2; };
    vector<int> arr = {2,3,7,4,9,12,5};
    auto it = find_if(arr.begin(), arr.end(), isOdd);

    cout << distance(arr.begin(), it) << endl;      // 1


    return 0;
}
```
---
#### find_if_not
`Iterator find_if_not(Iterator first, Iterator last, UnaryPredicate pred)`
+ 在範圍中找第一個不符合條件的元素，若沒有與之不符的元素則回傳 `last`。
```C++
#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int main(){
    vector<int> arr = {1,3,7,4,9,12,5};
    auto it1 = find_if_not(arr.begin(), arr.end(), [](int x)->bool{ return x < 8; });   // 4
    auto it2 = find_if_not(arr.begin(), arr.end(), [](int x)->bool{ return x < 13; });  // 7
    auto it3 = find_if_not(arr.begin(), arr.end(), [](int x)->bool{ return x == 1; });  // 1

    cout << distance(arr.begin(), it1) << endl;
    cout << distance(arr.begin(), it2) << endl;
    cout << distance(arr.begin(), it3) << endl;

    return 0;
}
```
---
#### find_end
`Iterator find_end(Iterator first1, Iterator last1, Iterator first2, Iterator last2)`
`Iterator find_end(Iterator first1, Iterator last1, Iterator first2, Iterator last2, BinaryPredicate pred)`
+ 在範圍中找出最後一次符合條件的子序列中的第一個元素。
```C++
#include <iostream>
#include <vector>
#include <algorithm>
#define all(x) x.begin(),x.end()

using namespace std;

int main(){
    vector<int> arr1 = {1,2,3,4,5,1,2,3,4,5};
    vector<int> arr2 = {1,2,3};
    vector<int> arr3 = {2,3,4};
    vector<int> arr4 = {4,5,6,4,5,6};
    vector<int> arr5 = {1,3,5};

    auto pred = [](int x, int y)->bool{ return x == y; };
    auto pred2 = [](int x, int y)->bool{ return x == y+3; };

    auto it1 = find_end(all(arr1), all(arr2));
    auto it2 = find_end(all(arr1), all(arr2), pred);
    auto it3 = find_end(all(arr1), all(arr3));
    auto it4 = find_end(all(arr1), all(arr4));
    auto it5 = find_end(all(arr4), all(arr2), pred2);
    auto it6 = find_end(all(arr1), all(arr5));

    cout << distance(arr1.begin(), it1) << endl;    // 5
    cout << distance(arr1.begin(), it2) << endl;    // 5
    cout << distance(arr1.begin(), it3) << endl;    // 6
    cout << distance(arr1.begin(), it4) << endl;    // 10
    cout << distance(arr4.begin(), it5) << endl;    // 3
    cout << distance(arr1.begin(), it6) << endl;    // 10

    return 0;
}
```
---
#### find_first_of
+ Find element from set in range
---
#### adjacent_find
+ Find equal adjacent elements in range
---
#### count
+ Count appearances of value in range
---
#### count_if
+ Return number of elements in range satisfying conditon
---
#### mismatch
+ Return first position where two ranges differ
---
#### equal
+ Test whether the elements in two ranges are equal
---
#### is_permutation
+ Test whether range is permutation of another
---
#### search
+ Search range for subsequence
---
#### search_n
+ Search range for elements
---

### 2. 有修改值的操作
### 3. 分割(partition)
### 4. 排序(sort)
### 5. 二分法(binary search)
### 6. 合併(merge)
### 7. 堆(heap)
### 8. 極值(min/max)
### 9. 其它
---
+ 回到目錄：[[Algo] 演算法筆記](/cs/algo)  
<!-- + 想要複習：[[Algo] 1-8. priority_queue](/cs/algo/priority_queue) -->