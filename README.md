# BIT-HRMS: Open Source Human Resource Management System

![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/dev-javid/bit-hrms/dotnet.yml?style=flat-square)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/dev-javid/bit-hrms?style=flat-square)
![GitHub](https://img.shields.io/github/license/dev-javid/bit-hrms?style=flat-square)
![GitHub last commit](https://img.shields.io/github/last-commit/dev-javid/bit-hrms?style=flat-square)

BIT-HRMS is a powerful, flexible, and user-friendly Human Resource Management System designed for small organizations. Built with .NET and React, this open-source solution offers a comprehensive set of features to streamline HR processes, enhance employee engagement, and drive organizational efficiency.

[![.NET](https://img.shields.io/badge/-.NET-512BD4?style=flat-square&logo=.net&logoColor=white)](#)
[![React](https://img.shields.io/badge/-React-61DAFB?style=flat-square&logo=react&logoColor=black)](#)
[![JWT Auth](https://img.shields.io/badge/-JWT%20Auth-000000?style=flat-square&logo=json-web-tokens&logoColor=white)](#)
[![Fluent Validation](https://img.shields.io/badge/-Fluent%20Validation-ff69b4?style=flat-square)](#)
[![Docker](https://img.shields.io/badge/-Docker-2496ED?style=flat-square&logo=docker&logoColor=white)](#)
[![PostgreSQL](https://img.shields.io/badge/-PostgreSQL-336791?style=flat-square&logo=postgresql&logoColor=white)](#)
[![TypeScript](https://img.shields.io/badge/-TypeScript-3178C6?style=flat-square&logo=typescript&logoColor=white)](#)
[![xUnit](https://img.shields.io/badge/-xUnit-8A2BE2?style=flat-square)](#)
[![Clean Architecture](https://img.shields.io/badge/-Clean%20Architecture-FF6C37?style=flat-square)](#)
[![Entity Framework Core](https://img.shields.io/badge/-Entity%20Framework%20Core-512BD4?style=flat-square)](#)
[![Tailwind](https://img.shields.io/badge/-Tailwind-38B2AC?style=flat-square&logo=tailwind-css&logoColor=white)](#)
[![Redux Toolkit](https://img.shields.io/badge/-Redux%20Toolkit-764ABC?style=flat-square&logo=redux&logoColor=white)](#)
[![shadcn/ui](https://img.shields.io/badge/-shadcn%2Fui-000000?style=flat-square)](#)


## 🚀 Key Features

- **Employee Management**: Easily manage employee information, contracts, and documents.
- **Time and Attendance**: Track work hours, manage leave requests, and monitor attendance.
- **Leave & Holidays**: Handles various types of leave (vacation, sick, personal, etc.). Set up company holidays.
- **Salary & Compensation**: Manage employee compensation & salary.
- **Income & Expenses**: Track organizational income and expenses.

## 🔧 Technical Details

- **Backend**: ASP.NET Core Web API
- **Frontend**: React with TypeScript
- **Database**: PostgreSQL
- **Authentication**: JWT-based authentication
- **API Documentation**: Swagger/OpenAPI
- **Containerization**: Docker support for easy deployment
- **Testing**: Unit and integration tests with xUnit
- **Logging**: Application logging to database using Serilog
- **Background Jobs**: Hangfire integration including hangfiore dashboard

## 🚀 Getting Started

To get started with BIT-HRMS, follow these steps:

1. Clone the repository:
   ```
   git clone https://github.com/dev-javid/bit-hrms.git
   ```
2. Navigate to the project directory:
   ```
   cd bit-hrms
   dotnet restore
   dotnet run
   ```
4. Set up your database (PostgreSQL)
5. Configure your app settings
5. Configure your app settings, you can also setup app configuration using Consul
6. Run the application (This will run both ackend api and front end application)
   ```
   dotnet restore
   dotnet run
   ```
6. Open the url http://localhost:5173 in the browser
7. Use default credentials username admin@example.com & password Password@123
8. View the backend logs http://localhost:5173/serilog-ui
9. View the background jobs http://localhost:5173/hangfire

For more detailed instructions, please refer to our [Installation Guide](link-to-installation-guide).

## 🤝 Contributing

We welcome contributions from the community! If you'd like to contribute, please:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

Please read our [Contributing Guidelines](link-to-contributing-guidelines) for more details.



## ⚠️ Disclaimer

This project is a work in progress and is not intended for production use at this time. It is being developed by an individual in their spare time as a learning exercise and to contribute to the open-source community. While efforts are made to ensure functionality and security, there may be bugs, incomplete features, or other issues.

**Use this software at your own risk.** The developer(s) of BIT-HRMS are not responsible for any damages, data loss, or other issues that may arise from the use of this software. Always backup your data and exercise caution when using any software, especially in a production environment.

## 🚧 Project Status: Alpha

BIT-HRMS is currently in the alpha stage of development. This means:

- Core features are still being developed and may change significantly
- The software may be unstable and could contain bugs
- Not all planned features are implemented yet
- Documentation may be incomplete or outdated

We welcome contributions and feedback, but please be aware of the project's current limitations.

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Contact

Dev Javid - [@dev_javid](https://twitter.com/dev_javid)

Project Link: [https://github.com/dev-javid/bit-hrms](https://github.com/dev-javid/bit-hrms)

---

Join us in revolutionizing HR management for small organizations. Whether you're an HR professional, a developer, or a small business owner, BIT-HRMS is designed with you in mind. Contribute, customize, and help us create the ultimate open-source HRMS solution!
