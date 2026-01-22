import { useEffect, useState } from "react";
import { getTodos, createTodo, deleteTodo } from "../api/todosApi";

export default function TodoPage() {
  const [todos, setTodos] = useState([]);
  const [title, setTitle] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    (async () => {
      try {
        const data = await getTodos();
        setTodos(data);
      } catch (e) {
        setError(e.message);
      }
    })();
  }, []);

  const onAdd = async () => {
    if (!title.trim()) return;
    try {
      const newTodo = await createTodo(title.trim());
      setTodos([...todos, newTodo]);
      setTitle("");
    } catch (e) {
      setError(e.message);
    }
  };

  const onDelete = async (id) => {
    try {
      await deleteTodo(id);
      setTodos(todos.filter((t) => t.id !== id));
    } catch (e) {
      setError(e.message);
    }
  };

  return (
    <div style={{ padding: 24, fontFamily: "sans-serif" }}>
      <hi> Todos </hi>
      {error && <div style={{ color: "red" }}>{error}</div>}

      <div style={{ display: "flex", gap: 8, marginBottom: 12 }}>
        <input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="New todo title"
        />
        <button onClick={onAdd}>Add</button>
      </div>

      <ul>
        {todos.map((t) => (
          <li key={t.id} style={{ display: "flex", gap: 8, margingBottom: 6 }}>
            <span>
              #{t.id} {t.title} {t.isDone ? "(done)" : ""}
            </span>
            <button onClick={() => onDelete(t.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
