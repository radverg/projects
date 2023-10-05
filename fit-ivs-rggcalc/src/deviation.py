
### HERE RESIDES MY TRY TO IMPORT FROM DIFFERENT DIRECTORY ###
########                    R. I. P.                  ########
########    The world is better place without you     ########
# import sys
# import os
# libdir = os.path.dirname(os.path.realpath(__file__))
# libdir += "/../src"
# print(libdir)
# sys.path.append(libdir)
# print(sys.path)

import sys
import rgg_mathlib

def run():
    # sum, count, deviation
    sumed=sumsqed=count = 0
    calc = rgg_mathlib.RggMathLib()

    if (len(sys.argv) < 2):
        for lines in sys.stdin:
            for words in lines.split():
                sumed = calc.add(sumed, (int(words)))
                sumsqed = calc.add(sumsqed,(calc.pow(int(words),2)))
                count += 1
    else:
        with open(sys.argv[1], "r") as numbersfile:
            for lines in numbersfile:
                for words in lines.split():
                    sumed = calc.add(sumed, (int(words)))
                    sumsqed = calc.add(sumsqed,(calc.pow(int(words),2)))
                    count += 1


    deviation = calc.root((calc.mul((calc.div(1,(calc.sub(count, 1)))),(calc.sub((sumsqed),(calc.mul((count),(calc.pow((calc.mul((calc.div(1,count)),(sumed))),(2))))))))),2)

    print(deviation)