# 如何批量实现通过MP3标题重命名文件名

本文介绍对MP3格式进行重命名操作，首先通过文件属性获取MP3文件标题Title，然后用标题对文件名进行重命名。例子如下：

- **处理前：**

![这里写图片描述](http://img.blog.csdn.net/20170518194559146?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvcXFfMjAxODM0ODk=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

- **处理后：**

![这里写图片描述](http://img.blog.csdn.net/20170518194618053?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvcXFfMjAxODM0ODk=/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/SouthEast)

方法通过C#实现，共分两步：

- **通过ShellClass获得文件属性**
- **以mp3文件的Title属性对其进行重命名**

-------------------

## 通过ShellClass获得文件属性

 1. 引用COM组件
Microsoft Shell Controls And Automation
***需要注意：***
DLL的属性Embed Interop Type 设为False，否则会引起互操作类型异常
 2. using Shell32
 3. 具体代码：
```
//file--文件名；icol--属性索引
static string getMusicName(string file,int iCol)
{
     ShellClass sh = new ShellClass();
     Folder dir = sh.NameSpace(Path.GetDirectoryName(file));
     FolderItem item = dir.ParseName(Path.GetFileName(file));

     string str = dir.GetDetailsOf(item, iCol);

     return str;               
}
```

 4.iCol 对应文件详细属性汇总

索引 | 属性名
----| --------------
0   | Name
1   | Size
2   | Type
3   | Date modified
4   | Date created
5   | Date accessed
6   | Attributes
7   | Offline status
8   | Offline availability
9   | Perceived type
10  | Owner
11  | Kinds
12  | Date taken
13  | Artists
14  | Album
15  | Year
16  | Genre
17  | Conductors
18  | Tags
19  | Rating
20  | Authors
21  | Title
22  | Subject
23  | Categories
24  | Comments
25  | Copyright
26  | #
27  | Length
28  | Bit rate
29  | Protected
30  | Camera model
31  | Dimensions
32  | Camera maker
33  | Company
34  | File description
35  | Program name
36  | Duration
37  | Is online
38  | Is recurring
39  | Location
40  | Optional attendee addresses
41  | Optional attendees
42  | Organizer address
43  | Organizer name
44  | Reminder time
45  | Required attendee addresses
46  | Required attendees
47  | Resources
48  | Free/busy status
49  | Total size
50  | Account name
51  | Computer
52  | Anniversary
53  | Assistant's name
54  | Assistant's phone
55  | Birthday
56  | Business address
57  | Business city
58  | Business country/region
59  | Business P.O. box
60  | Business postal code
61  | Business state or province
62  | Business street
63  | Business fax
64  | Business home page
65  | Business phone
66  | Callback number
67  | Car phone
68  | Children
69  | Company main phone
70  | Department
71  | E-mail Address
72  | E-mail2
73  | E-mail3
74  | E-mail list
75  | E-mail display name
76  | File as
77  | First name
78  | Full name
79  | Gender
80  | Given name
81  | Hobbies
82  | Home address
83  | Home city
84  | Home country/region
85  | Home P.O. box
86  | Home postal code


 


 
 

## 以mp3文件的Title属性对其进行重命名

> C#里面，重命名文件时，没有 rename 这个功能，使用的是FileInfo.MoveTo的方式，MoveTo 到原目录里一个新的名字，即实现了重命名。

基本代码：

```
static void renameFile(string dirName, string oldName, string newName)
{
    FileInfo fi = new FileInfo(dirName + oldName);
    fi.MoveTo(Path.Combine(dirName + newName));
}

```

---------