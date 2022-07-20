'''
寻找最长子序列
'''

'''
算法原理
做一个二维表格C
c[i][j] =   0   i = 0 或 j = 0
            c[i-1][j-1] + 1   i,j > 0 且 X[i] = Y[j]
            max(c[i][j-1],c[i-1][j]) i,j > 0 且 X[i] <> Y[j]
'''
sequenceA = "ACDFA"
sequenceB = "AFAHT"
ltsTable = []
directionTable = []


'''
创建一个表格，用来查找最长子序列
'''
def createTable(seqALength:int, seqBLength:int):
    # todo: 用 ltsTable directionTable 这两个变量
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
            else:
                if sequenceA[i-1] == sequenceB[j - 1]:
                    ltsTable[i][j] = ltsTable[i-1][j-1] + 1
                    directionTable[i][j] = 0 # 继承左上的值
                else:
                    ltsTable[i][j] = max(ltsTable[i-1][j], ltsTable[i][j-1])
                    if ltsTable[i-1][j] > ltsTable[i][j-1]:
                        directionTable[i][j] = 1 # 继承正上面的值
                    elif ltsTable[i-1][j] == ltsTable[i][j-1]:
                        # 注意，此时结果不唯一
                        directionTable[i][j] = 1 # 继承正上面的值
                    elif ltsTable[i-1][j] < ltsTable[i][j-1]:
                        directionTable[i][j] = 2 # 继承左面的值
                    else:
                        pass # 不可能出现

    # todo: 填好 ltsTable directionTable 这两个表
    pass

'''
输出结果
'''
def findLTS():
    # 输出LTS（含结果文件）
    pass

def main():
    createTable(len(sequenceA), len(sequenceB))
    analyzeSequence()
    findLTS()