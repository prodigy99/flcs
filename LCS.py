'''
寻找最长子序列
'''

'''
算法原理
做一个二维表格，其行列分别为字符串A和B
c[i][j] =   0   i = 0 或 j = 0
            c[i-1][j-1] + 1   i,j > 0 且 X[i] = Y[j]
            max(c[i][j-1],c[i-1][j]) i,j > 0 且 X[i] <> Y[j]
'''
from getopt import getopt
import sys

sequenceA = ""
sequenceB = ""
ltsTable = []
directionTable = []

file1Path = ""
file2Path = ""
printLCSIndexInFile1 = False # 将最长公共子序列中元素在FILE1中的行号输出到stdout
printLCSIndexInFile2 = False # 将最长公共子序列中元素在FILE2中的行号输出到stdout

'''
创建一个表格，用来查找最长子序列
'''
def createTable(seqALength:int, seqBLength:int):
    for i in range(seqALength + 1):
        ltsTable.append([0 for j in range(seqBLength + 1)])
        directionTable.append([0 for j in range(seqBLength + 1)])
'''
分析输入的两个字符串并填写表格
'''
def analyzeSequence(seqALength:int, seqBLength:int):
    for i in range(seqALength + 1):
        for j in range(seqBLength + 1):
            if i == 0 or j == 0:
                ltsTable[i][j] = 0
                print(0)

            else:
                if sequenceA[i-1] == sequenceB[j - 1]:
                    ltsTable[i][j] = ltsTable[i-1][j-1] + 1
                    directionTable[i][j] = 0 # 继承左上的值
                    print(1)
                else:
                    ltsTable[i][j] = max(ltsTable[i-1][j], ltsTable[i][j-1])
                    print(2)
                    if ltsTable[i-1][j] > ltsTable[i][j-1]:
                        directionTable[i][j] = 1 # 继承正上面的值
                    elif ltsTable[i-1][j] == ltsTable[i][j-1]:
                        # 注意，此时结果不唯一
                        directionTable[i][j] = 1 # 继承正上面的值
                    elif ltsTable[i-1][j] < ltsTable[i][j-1]:
                        directionTable[i][j] = 2 # 继承左面的值
                    else:
                        pass # 不可能出现这种情况，两个数值的关系只可能是大于、小于或者等于
    pass

'''
输出结果
'''
def findLTS():
    i = len(sequenceA)
    j = len(sequenceB)

    lcsArr = []
    Aindex = []
    Bindex = []
    while i != 0 and j != 0:
        if sequenceA[i-1] == sequenceB[j-1] :
            lcsArr.insert(0, sequenceA[i-1])
            Aindex.insert(0, i - 1)
            Bindex.insert(0, j - 1)
        
        if directionTable[i][j] == 0 :
            i = i - 1
            j = j - 1
        elif directionTable[i][j]==1:
            i = i - 1
        else:
            j = j - 1
    
    print("LCS：")
    for k in lcsArr : print(k)

    if printLCSIndexInFile1 == True :
        print("LCS元素在文件1中的行号（从1开始）：")
        for i in Aindex : print(i)
    if printLCSIndexInFile2 == True :
        print("LCS元素在文件2中的行号（从1开始）：")
        for j in Bindex : print(j)


'''
处理CLI提供的参数
'''
def parseCmdParams():
    global sequenceA, sequenceB
    global printLCSIndexInFile1, printLCSIndexInFile2
    options, args = getopt(sys.argv[1:], "", ["l1", "l2"])
    # print(options)
    for opt, value in options:

        if ("--help", "-h") in list(opt):
            print("本程序暂无帮助信息")
        if "--l1" in opt:
            printLCSIndexInFile1 = True
        if "--l2" in opt:
            printLCSIndexInFile2 = True
        # print(opt, value)

    try:
        file1Path = args[0]
        file2Path = args[1]

        file1 = open(file1Path, 'r')
        sequenceA = list(map(lambda str:str.strip(),file1.readlines()))
        file2 = open(file2Path, 'r')
        sequenceB = list(map(lambda str:str.strip(),file2.readlines()))
        file1.close()
        file2.close()

    except IndexError as e1:
        print("无法确定文件1或文件2的路径，使用测试序列ABCDEF与ACEFHB")
        sequenceB = "ABCDEF"
        sequenceA = "ACEFHB"
    except IOError as e2:
        print("无法打开文件1或文件2")
        exit(-1)

if __name__ == "__main__":
    parseCmdParams()
    createTable(len(sequenceA), len(sequenceB))
    analyzeSequence(len(sequenceA), len(sequenceB))
    findLTS()

else:
    print("请在命令提示符（PowerShell、终端等CLI界面下）直接运行此程序。")