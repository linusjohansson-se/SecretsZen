export default function AboutPage() {
  return (
    <div className="flex flex-col gap-4">
      <h2 className="text-2xl font-bold tracking-tight text-foreground">How it works</h2>
      <p className="text-sm text-muted-foreground">SecretsZen uses encryption to ensure that we can never read your secrets. Only someone with the link can decrypt and view it.</p>
    </div>
  )
}
