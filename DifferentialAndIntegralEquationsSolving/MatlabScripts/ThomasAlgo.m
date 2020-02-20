function [x,y] = ThomasAlgo(x0,  x1, h,  y0) 

N = (x1 - x0)/h;
X = zeros(1,N);
 for i =1:N
     X(i) = x0 + i * h;
 end
%  to be added [x,y] = ode23(@(x,y) 4*y^2+2, X, y0);
