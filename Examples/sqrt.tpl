﻿{ (z >= 1) }
(
	(
		( x := 1 ; y := 1 )
		;
		( 
			{ 
				(
					(y = (x * x))  
					&& 
					(((x - 1) * (x - 1)) <= z)
				) 
			} 
			while (y <= z) do 
			( 
				y := (y + ((2 * x) + 1)) 
				; 
				x := (x + 1) 
			) 
		)
	)
	;
	x := (x - 1)
)
{ 
	(
		((x * x) <= z) 
		&& 
		(((x + 1) * (x + 1)) > z)
	) 
}