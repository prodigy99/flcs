
'''
做一个二维表格C
c[i][j] =   0   i = 0 或 j = 0
            c[i-1][j-1]+1   i,j > 0 且 X[i] = Y
            max(c[i][j-1],c[i-1][j])
'''