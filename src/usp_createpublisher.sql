create function usp_createpublisher(p_id out numeric
,p_name varchar,
createddt timestamp) as '
begin
 Insert into Publishers(publisher,created)values(p_name,createddt);
 Select  MAX(publisherid) into p_id FROM Publishers;
end;' 
language 'plpgsql';