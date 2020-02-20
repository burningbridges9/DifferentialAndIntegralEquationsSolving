function [x,y] = Euler(x0,  x1, h,  y0) 

N = (x1 - x0)/h;
X = zeros(1,N);
 for i =1:N
     X(i) = x0 + i * h;
 end
[x,y] = ode23(@(x,y) x-y+2, X, y0);

