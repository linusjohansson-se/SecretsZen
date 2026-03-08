import Footer from "@/features/footer/components/Footer"
import Header from "@/features/header/components/Header"

interface Props {
  children: React.ReactNode
}

export default function RootLayout({ children }: Props) {
  return (
    <div className="min-h-screen flex flex-col">
      <Header />
      <main className="flex-1 flex items-center justify-center px-4 py-8">
        <div className="w-full max-w-lg">
          {children}
        </div>
      </main>
      <Footer />
    </div>
  )
}
