public class FannkuchRedux {
    public static int fannkuch(int n) {
        int[] perm = new int[n];
        int[] perm1 = new int[n];
        int[] count = new int[n];
        int maxFlipsCount = 0;
        int permCount = 0;
        int checksum = 0;

        for(int i=0; i<n; i++) perm1[i] = i;
        int r = n;

        while (true) {

            while (r != 1){ count[r-1] = r; r--; }

            for(int i=0; i<n; i++) perm[i] = perm1[i];
            int flipsCount = 0;
            int k;

            while ( !((k=perm[0]) == 0) ) {
                int k2 = (k+1) >> 1;
                for(int i=0; i<k2; i++) {
                    int temp = perm[i]; perm[i] = perm[k-i]; perm[k-i] = temp;
                }
                flipsCount++;
            }

            maxFlipsCount = Math.max(maxFlipsCount, flipsCount);
            checksum += permCount%2 == 0 ? flipsCount : -flipsCount;

            // Use incremental change to generate another permutation
            while (true) {
                if (r == n) {
                    //System.out.println( checksum );
                    return checksum;
                }
                int perm0 = perm1[0];
                int i = 0;
                while (i < r) {
                    int j = i + 1;
                    perm1[i] = perm1[j];
                    i = j;
                }
                perm1[r] = perm0;

                count[r] = count[r] - 1;
                if (count[r] > 0) break;
                r++;
            }

            permCount++;
        }
    }

    public static int fannkuchRedux(long loopIterations){
        int result = 0;
        int n = 12;
        for(long i = 0; Long.compareUnsigned(i, loopIterations) < 0; i++){
            result += fannkuch(n);
        }

        return result;
    }
}
