﻿{ (n > 0) }
(
	(
		a := 1
		;
		b := 1
	)
	;
	(
		{ (a = (b!)) }
		while (b < n) 
		do
		(
			a := (a * n)
			;
			b := (b + 1)
		)
	)
)
{ ((n!) = a) }