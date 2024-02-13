SELECT *
FROM employer
WHERE salary = (SELECT MAX(salary) FROM employer);

WITH RECURSIVE managers AS (
  SELECT id, chief_id, 1 AS depth
  FROM employer
  WHERE chief_id IS NULL
  UNION ALL
  SELECT em.id, em.chief_id, m.depth + 1
  FROM employer as em
  INNER JOIN managers m ON e.chief_id = m.id
)
SELECT MAX(depth) AS max_depth
FROM managers;

SELECT d.name AS department_name, SUM(e.salary) AS total_salary
FROM department AS  d
JOIN employer e ON d.id = e.department_id
GROUP BY d.name
ORDER BY total_salary DESC
LIMIT 1;

SELECT *
FROM employer
WHERE name LIKE 'Ð%í';

