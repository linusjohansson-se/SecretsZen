import { createFileRoute } from '@tanstack/react-router'
import SecretsPage from '@/pages/SecretsPage'

export const Route = createFileRoute('/')({
  component: SecretsPage,
})
