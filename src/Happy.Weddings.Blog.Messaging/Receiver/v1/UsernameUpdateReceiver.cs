using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Happy.Weddings.Blog.Core.DTO;
using Happy.Weddings.Blog.Core.Infrastructure;
using Happy.Weddings.Blog.Core.Services;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Happy.Weddings.Blog.Messaging.Receiver.v1
{
    public class UsernameUpdateReceiver : BackgroundService
    {
        /// <summary>
        /// The channel
        /// </summary>
        private IModel channel;

        /// <summary>
        /// The connection
        /// </summary>
        private IConnection connection;

        /// <summary>
        /// The usser name update service
        /// </summary>
        private readonly IUserNameUpdateService userNameUpdateService;

        /// <summary>
        /// The hostname
        /// </summary>
        private readonly string hostname;

        /// <summary>
        /// The queue name
        /// </summary>
        private readonly string queueName;

        /// <summary>
        /// The exchange name
        /// </summary>
        private readonly string exchangeName;

        /// <summary>
        /// The username
        /// </summary>
        private readonly string username;

        /// <summary>
        /// The password
        /// </summary>
        private readonly string password;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsernameUpdateReceiver" /> class.
        /// </summary>
        /// <param name="userNameUpdateService">The user name update service.</param>
        /// <param name="rabbitMqOptions">The rabbit mq options.</param>
        public UsernameUpdateReceiver(
            IUserNameUpdateService userNameUpdateService, 
            RabbitMqConfig rabbitMqOptions)
        {
            this.userNameUpdateService = userNameUpdateService;
            hostname = rabbitMqOptions.Hostname;
            queueName = rabbitMqOptions.QueueName;
            exchangeName = rabbitMqOptions.ExchangeName;
            username = rabbitMqOptions.UserName;
            password = rabbitMqOptions.Password;
            InitializeRabbitMqListener();
        }

        /// <summary>
        /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts. The implementation should return a task that represents
        /// the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.
        /// </returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var content = Encoding.UTF8.GetString(body);
                var updateCustomerFullNameModel = JsonConvert.DeserializeObject<UpdateUserFullNameModel>(content);

                HandleMessage(updateCustomerFullNameModel);

                channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            channel.BasicConsume(queueName, false, consumer);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes the rabbit mq listener.
        /// </summary>
        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = hostname,
                UserName = username,
                Password = password
            };

            connection = factory.CreateConnection();
            connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="updateUserFullNameModel">The update user full name model.</param>
        private void HandleMessage(UpdateUserFullNameModel updateUserFullNameModel)
        {
            userNameUpdateService.UpdateUserNameInStories(updateUserFullNameModel);
        }

        /// <summary>
        /// Called when [consumer consumer cancelled].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ConsumerEventArgs"/> instance containing the event data.</param>
        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        /// <summary>
        /// Called when [consumer unregistered].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ConsumerEventArgs"/> instance containing the event data.</param>
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        /// <summary>
        /// Called when [consumer registered].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ConsumerEventArgs"/> instance containing the event data.</param>
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        /// <summary>
        /// Called when [consumer shutdown].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ShutdownEventArgs"/> instance containing the event data.</param>
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        /// <summary>
        /// Handles the ConnectionShutdown event of the RabbitMQ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ShutdownEventArgs"/> instance containing the event data.</param>
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            channel.Close();
            connection.Close();
            base.Dispose();
        }
    }
}
