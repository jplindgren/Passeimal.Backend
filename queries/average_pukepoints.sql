select avg(qty) from 
( select Place_id, Sum(Severity) as qty from Pukes
group by Place_id ) as result