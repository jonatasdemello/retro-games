#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

int main(int argc, char* argv[])
{
    long n, num;
    double x, xi;
    x = 0.1;
    num = 100;

    if (argc > 1) {
        int input = atoi(argv[1]);
        /* printf("input %d",input); */
        num = input;
    }

    for (n=0; n<num; n++) {
        x = 100 * log(x);
        xi = trunc(x);
        x = fabs(x-xi);
        printf("%.16f\n",x);
    }
    return 0;
}
