import { Link } from "@tanstack/react-router"
import { LockKeyhole } from "lucide-react"

export default function Footer() {
  return (
    <footer className="border-t border-border px-4 py-3">
      <p className="flex items-center gap-1.5 text-sm text-muted-foreground">
        <LockKeyhole size={14} />
        We can't read your secrets. Only someone with the link can.
        <Link to="/about" className="underline underline-offset-4 hover:text-foreground transition-colors">Learn more</Link>
      </p>
    </footer>
  )
}
