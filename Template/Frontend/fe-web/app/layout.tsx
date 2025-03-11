import type { Metadata } from 'next'
import '@/app/globals.css'

export const metadata: Metadata = {
  title: 'NexaDash',
  description: 'Generated by AppStoneLab',
}

export default async function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode
}>) {
  return (
    <html lang="en" className="scroll-smooth">
      <body className="bg-gray-400 font-plus-jakarta text-sm/[22px] font-normal text-gray antialiased">
        {children}
      </body>
    </html>
  )
}
