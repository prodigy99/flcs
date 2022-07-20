'''
寻找最长子序列
'''

'''
算法原理
做一个二维表格C
c[i][j] =   0   i = 0 或 j = 0
            c[i-1][j-1]+1   i,j > 0 且 X[i] = Y
            max(c[i][j-1],c[i-1][j])
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
    pass

def analyzeSequence():
    # todo: 填好 ltsTable directionTable 这两个表
    pass

def findLTS():
    # 输出LTS（含结果文件）
    pass

def main():
    createTable()
    analyzeSequence()
    findLTS()