﻿{ ((a >= 0) && (b >= 0)) }
(
	(
		(
			x := a 
			;
			y := b
		)
		;
		z := 0
	)
	;
	(
		{ (((x * y) + z) = (a * b)) }
		while (y > 0)
		do
		(
			(
				(
					if ((y % 2) = 0)
					then
						z := (z + x)
					else
						z := z
				)
				;
				x := (2 * x)
			)
			;
			y := (y / 2)
		)
	)
)
{ (z = (a * b)) }